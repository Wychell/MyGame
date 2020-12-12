using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyGame.Infra.Security
{
    public class JwtTokenProvider : IJwtTokenProvider
    {
        public IConfiguration Configuration { get; }
        public JwtTokenProvider(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string GenerateToken(User usuario, int expireTime = 24)
        {
            var symmetricKey = Convert.FromBase64String(Configuration["TokenConfigurations:Secretkey"]);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.Now;
            var claims = new List<Claim>() {
                    new Claim("id", usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Name),
                    new Claim(ClaimTypes.Email, usuario.Email)};


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = now.AddHours(expireTime),
                Issuer = Configuration["TokenConfigurations:Issuer"],
                Audience = Configuration["TokenConfigurations:Audience"],
                IssuedAt = now,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };


            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }
    }
}
