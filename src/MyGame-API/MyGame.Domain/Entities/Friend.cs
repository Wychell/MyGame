using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyGame.Domain.Entities
{
    public class Friend : Entity
    {
        private readonly ICollection<Loan> _loans = new HashSet<Loan>();

        protected Friend()
        {

        }
        public Friend(string name, string email, string phone, Guid userId)
        {
            Name = name;
            Email = email;
            Phone = phone;
            UserId = userId;
            Validate();
        }
        public Friend(string name, string email, string phone, User user) : this(name, email, phone, user.Id)
        {
            User = user;
        }


        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public IReadOnlyCollection<Loan> Loans => (IReadOnlyCollection<Loan>)_loans;

        public void Update(string name, string email, string phone)
        {
            Name = name;
            Email = email;
            Phone = phone;

            Validate();
        }

        private void Validate()
        {
            AddNotifications(new Contract()
                      .Requires()
                      .IsNotNullOrEmpty(Name, "Nome", "Nome é obrigatório")
                      .IsEmail(Email, "Email", "Email invalido")
                      .IsNotNullOrEmpty(Phone, "Telefone", "O Telefone é obrigatório"));
        }

        public void AddLoan(Loan loan)
        {
            if (!_loans.Any(x => x.GameId == loan.GameId && !x.EndDate.HasValue))
                _loans.Add(loan);
            else
                AddNotification("Loan", "existem empréstimos ativos para este jogo");
        }


        public void FinalizeLoan(Guid LoandId)
        {
            var loan = _loans.FirstOrDefault(x => x.Id == LoandId);

            if (loan != null && !loan.EndDate.HasValue)
                loan.EndLoan();
            else if (loan != null && loan.EndDate.HasValue)
                AddNotification("Loan", "Emprestimo já finalizado");
            else
                AddNotification("Loan", "Emprestimo não encontrado");

        }

        public void RevokeAllLoan()
        {
            foreach (var loan in _loans)
                loan.EndLoan();
        }

    }
}
