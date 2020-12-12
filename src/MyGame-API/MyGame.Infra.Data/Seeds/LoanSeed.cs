using Microsoft.EntityFrameworkCore;
using MyGame.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MyGame.Infra.Data.Seeds
{
    public class LoanData : ForceDataSeed
    {

        public static List<Loan> GetLoans()
        {
            return new List<Loan>
            {
                ForceId(Guid.Parse("a7394a7a-358c-4d30-ac79-86725ce2cbb0"), new Loan(FriendData.AmigoDaOnca.Id,GameData.FIFA.Id)),
                ForceId(Guid.Parse("4cc3e40d-b70e-4258-997b-c51abe6b26fc"), new Loan(FriendData.AmigoDaCobra.Id,GameData.GTA.Id)),
            };
        }
    }


    public class LoanSeed : Seed<Loan>
    {
        public LoanSeed(ModelBuilder modelBuilder) : base(modelBuilder)
        {
        }

        public override void Execute()
        {
            var list = LoanData.GetLoans();
            ForceDate(list);
            Entity.HasData(list);
        }
    }
}
