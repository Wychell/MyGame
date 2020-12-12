using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyGame.Application.ApplicationServices;
using MyGame.Application.ApplicationServices.Interface;
using MyGame.Application.ApplicationServices.NotificationContext;
using MyGame.Application.Mapper;
using MyGame.Domain.Repositories;
using MyGame.Infra.Data.Context;
using MyGame.Infra.Data.Repositories;
using MyGame.Infra.Security;
using System;

namespace MyGame.Infra.Ioc
{
    public static class DependecyContext
    {
        public static void AddAplicationContext(this IServiceCollection services, IConfiguration configuration, Type StartupType)
        {

            services
              .AddDbContext<MyGameContext>(optionsBuilder =>
              {
                  optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
                  {
                      sqlOptions.MigrationsAssembly("MyGame.Infra.Data");
                  });
              });

            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();
            services.AddScoped<INotificationContext, NotificationContext>();
            services.AddScoped<IAuthUserResolver, AuthUserResolver>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFriendRepository, FriendRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();

            services.AddScoped<IGameApplicationService, GameApplicationService>();
            services.AddScoped<IUsuarioApplicationService, UsuarioApplicationService>();
            services.AddScoped<IFriendApplicationService, FriendApplicationService>();

            services.AddAutoMapper(StartupType);
            services.AddSingleton(ctx => AutoMapperConfig.RegisterMappings().CreateMapper());

            services.AddSwaggerContext();
            services.AddAuthContext(configuration);
        }

        private static void AddAuthContext(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenConfigurations = configuration.GetSection("TokenConfigurations").Get<TokenConfigurations>();
            var symmetricKey = Convert.FromBase64String(tokenConfigurations.Secretkey);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearerOptions =>
            {

                bearerOptions.RequireHttpsMetadata = false;


                var paramsValidation = bearerOptions.TokenValidationParameters;

                paramsValidation.SaveSigninToken = true;
                paramsValidation.ValidateIssuer = true;

                paramsValidation.IssuerSigningKey = new SymmetricSecurityKey(symmetricKey);
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                paramsValidation.ValidateAudience = true;

                paramsValidation.ValidateIssuerSigningKey = true;

                paramsValidation.ValidateLifetime = true;


                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorizationCore(c =>
            {
                c.DefaultPolicy = MyGamePolicy.policy;
                c.AddPolicy("Bearer", MyGamePolicy.policy);
            });
        }

        public static void AddSwaggerContext(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                  });

            });
        }

        public static void UseApplicationContext(this IApplicationBuilder app)
        {
            app.UseSwaggerContext();
            app.UseAuthentication();
        }


        public static void UseSwaggerContext(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                "Swagger Demo API v1");
            });
        }
    }
}
