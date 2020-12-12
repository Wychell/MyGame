using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyGame.Domain.Entities
{
    public class Game : Entity
    {
        private readonly ICollection<Loan> _loans = new HashSet<Loan>();

        protected Game()
        {

        }
        public Game(string name, string gender)
        {
            Name = name;
            Gender = gender;

            ValiDate();
        }

        private void ValiDate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(Name, "Nome", "Nome é obrigatório")
                .IsNotNullOrEmpty(Gender, "Gender", "Gênero é obrigatório"));
        }

        public string Name { get; private set; }
        public string Gender { get; private set; }

        public IReadOnlyCollection<Loan> Loans => (IReadOnlyCollection<Loan>)_loans;

        public void Update(string name, string gender)
        {
            Name = name;
            Gender = gender;
            ValiDate();
        }
    }
}
