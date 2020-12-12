using FluentAssertions;
using MyGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MyGames.Unit.Test
{
    public class FriendTest
    {

        [Fact]
        public void DeveCriarAmigoValido()
        {
            var amigo = new Friend("amigo1", "amigo@gmail.com", "3333", new User("mychell", "mychell.mds@gmail.com"));
            amigo.Valid.Should().BeTrue();
        }

        [Fact]
        public void DeveValidarEmailErradoAmigoInvalido()
        {
            var amigo = new Friend("amigo1", "amigogmail.com", "3333", new User("mychell", "mychell.mds@gmail.com"));
            amigo.Notifications.Any(x => x.Message.Contains("Email invalido")).Should().BeTrue();
            amigo.Valid.Should().BeFalse();
        }

        [Fact]
        public void DeveValidarEmailVazioAmigoInvalido()
        {
            var amigo = new Friend("amigo1", "", "3333", new User("mychell", "mychell.mds@gmail.com"));
            amigo.Notifications.Any(x => x.Message.Contains("Email invalido")).Should().BeTrue();
            amigo.Valid.Should().BeFalse();
        }

        [Fact]
        public void DeveValidarNomeAmigoInvalido()
        {
            var amigo = new Friend("", "amigo@gmail.com", "3333", new User("mychell", "mychell.mds@gmail.com"));
            amigo.Notifications.Any(x => x.Message.Contains("Nome é obrigatório")).Should().BeTrue();
            amigo.Valid.Should().BeFalse();
        }

        [Fact]
        public void DeveAdicionarEmprestimo()
        {
            var amigo = new Friend("amigo1", "amigo@gmail.com", "3333", new User("mychell", "mychell.mds@gmail.com"));
            var jogo = new Game("GTA", "Aventura");

            amigo.AddLoan(new Loan(amigo, jogo));

            amigo.Loans.Count.Should().Be(1);
            amigo.Valid.Should().BeTrue();
        }

        [Fact]
        public void DeveValidarJogoAindaEmprestado()
        {
            var amigo = new Friend("amigo1", "amigo@gmail.com", "3333", new User("mychell", "mychell.mds@gmail.com"));
            var jogo = new Game("GTA", "Aventura");
            var emprestimo = new Loan(amigo, jogo);


            amigo.AddLoan(emprestimo);
            amigo.AddLoan(emprestimo);

            amigo.Valid.Should().BeFalse();
            amigo.Notifications.Any(x => x.Message.Contains("existem empréstimos ativos para este jogo"));
        }

        [Fact]
        public void DeveAdicionarMesmoJogoComEmprestimoFinalizado()
        {
            var amigo = new Friend("amigo1", "amigo@gmail.com", "3333", new User("mychell", "mychell.mds@gmail.com"));
            var jogo = new Game("GTA", "Aventura");
            var emprestimo = new Loan(amigo, jogo);


            amigo.AddLoan(emprestimo);
            emprestimo.EndLoan();
            amigo.AddLoan(emprestimo);

            amigo.Valid.Should().BeTrue();
        }


        [Fact]
        public void DeveRevogarTodosOsJogosEmprestadoParaoAmigo()
        {
            var amigo = new Friend("amigo1", "amigo@gmail.com", "3333", new User("mychell", "mychell.mds@gmail.com"));
            var jogo = new Game("GTA", "Aventura");
            var jogo2 = new Game("COD", "Aventura");


            amigo.AddLoan(new Loan(amigo, jogo));
            amigo.AddLoan(new Loan(amigo, jogo2));

            amigo.RevokeAllLoan();

            amigo.Loans.All(x => x.EndDate.HasValue).Should().BeTrue();

            amigo.Valid.Should().BeTrue();
        }

        [Fact]
        public void DeveFinalizarEmprestioAtivo()
        {
            var amigo = new Friend("amigo1", "amigo@gmail.com", "3333", new User("mychell", "mychell.mds@gmail.com"));
            var jogo = new Game("GTA", "Aventura");
            var jogo2 = new Game("COD", "Aventura");
            var emprestimo = new Loan(amigo, jogo);


            amigo.AddLoan(emprestimo);

            amigo.FinalizeLoan(emprestimo.Id);

            amigo.Loans.All(x => x.EndDate.HasValue).Should().BeTrue();

            amigo.Valid.Should().BeTrue();
        }

        [Fact]
        public void NaoDeveFinalizarEmprestimoJaFinalizado()
        {
            var amigo = new Friend("amigo1", "amigo@gmail.com", "3333", new User("mychell", "mychell.mds@gmail.com"));
            var jogo = new Game("GTA", "Aventura");
            var jogo2 = new Game("COD", "Aventura");
            var emprestimo = new Loan(amigo, jogo);

            amigo.AddLoan(emprestimo);
            amigo.FinalizeLoan(emprestimo.Id);
            emprestimo.EndLoan();
            amigo.FinalizeLoan(emprestimo.Id);

            amigo.Valid.Should().BeFalse();

            amigo.Notifications.Any(x => x.Message.Contains("Emprestimo já finalizado")).Should().BeTrue();
        }

    }
}
