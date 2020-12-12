using Microsoft.EntityFrameworkCore;
using MyGame.Infra.Data.Configurations;
using MyGame.Infra.Data.Seeds;
using System;

namespace MyGame.Infra.Data.Context
{
    public class MyGameContext : DbContext
    {
        public MyGameContext(DbContextOptions<MyGameContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
            new UserSeed(modelBuilder);
            new FriendsSeed(modelBuilder);
            new GamesSeed(modelBuilder);
            new LoanSeed(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
     
    }

  
}

