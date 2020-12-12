using Microsoft.EntityFrameworkCore;
using MyGame.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MyGame.Infra.Data.Seeds
{
    public  class GameData : ForceDataSeed
    {
        public static Game GTA = ForceId(Guid.Parse("7eef25b8-ffd4-4b59-b183-edf36dc33d2a"), new Game("GTA V", "Aventura"));
        public static Game FIFA = ForceId(Guid.Parse("21eaa1fa-bdf4-496a-85ac-ce21da56cff9"), new Game("FIFA 21", "Esporte"));
    }

    public class GamesSeed : Seed<Game>
    {
        public GamesSeed(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }

        public override void Execute()
        {
            var lista = new List<Game> { GameData.FIFA, GameData.GTA };
            ForceDate(lista);
            Entity.HasData(lista);
        }
    }
}
