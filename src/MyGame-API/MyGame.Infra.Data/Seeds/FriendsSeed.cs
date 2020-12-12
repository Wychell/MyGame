using Microsoft.EntityFrameworkCore;
using MyGame.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MyGame.Infra.Data.Seeds
{
    public  class FriendData : ForceDataSeed
    {
        public static Friend AmigoDaOnca = ForceId(Guid.Parse("603c8ac5-2fc5-491f-9fb4-0924320b4301"), new Friend("Amigo da Onça", "amigo@gmail.com", "333333", UserDate.Admin.Id));
        public static Friend AmigoDaCobra = ForceId(Guid.Parse("dfb45e41-045d-4fdd-a408-2bba3d98b890"), new Friend("Amigo da Cobra", "cobra@gmail.com", "444444", UserDate.Admin.Id));
    }

    public class FriendsSeed : Seed<Friend>
    {
        public FriendsSeed(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }

        public override void Execute()
        {
            var lista = new List<Friend> { FriendData.AmigoDaOnca, FriendData.AmigoDaCobra };
            ForceDate(lista);
            Entity.HasData(lista);
        }
    }
}
