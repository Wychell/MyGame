using Microsoft.EntityFrameworkCore;
using MyGame.Domain.Entities;
using System;

namespace MyGame.Infra.Data.Seeds
{
    public class UserDate : ForceDataSeed
    {
        public static User Admin = ForceId(Guid.Parse("7b154c78-d523-48f9-b0b0-7b9b9e9cc65f"), new User("Mychell", "mychell.mds@gmail.com", "password"));

    }

    public class UserSeed : Seed<User>
    {
        public UserSeed(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }

        public override void Execute()
        {
            var user = UserDate.Admin;
            var prop = user.GetType().GetProperty("CreateDate");
            prop.SetValue(user, new DateTime(2020, 12, 13));
            Entity.HasData(user);
        }
    }
}
