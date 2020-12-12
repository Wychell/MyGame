using FluentAssertions;
using MyGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MyGames.Unit.Test
{
    public class LoanTest
    {
        [Fact]
        public void DeveCriarJogo()
        {
            var amigo = new Friend("1", "teste@teste.com", "333", new User("mychell", "mychell.mds@gmail.com"));
            var jogo = new Game("GTA V", "Aventura");
            var emprestimo = new Loan(amigo, jogo);

            emprestimo.Valid.Should().BeTrue();
        }

        [Fact]
        public void DeveValidarEmprestimoSemAmigo()
        {
            var jogo = new Game("GTA V", "Aventura");
            var emprestimo = new Loan(Guid.Empty, jogo.Id);

            emprestimo.Valid.Should().BeFalse();
            emprestimo.Notifications.Any(x => x.Message.Contains("Amigo é necessário")).Should().BeTrue();
        }

        [Fact]
        public void DeveValidarEmprestimoSemJogo()
        {
            var amigo = new Friend("1", "teste@teste.com", "333", new User("mychell", "mychell.mds@gmail.com"));
            var emprestimo = new Loan(amigo.Id, Guid.Empty);

            emprestimo.Valid.Should().BeFalse();
            emprestimo.Notifications.Any(x => x.Message.Contains("O jogo é obrigatório")).Should().BeTrue();
        }


        [Fact]
        public void DeveFinalizarEmprestimo()
        {
            var amigo = new Friend("1", "teste@teste.com", "333", new User("mychell", "mychell.mds@gmail.com"));
            var jogo = new Game("GTA V", "Aventura");
            var emprestimo = new Loan(amigo, jogo);

            emprestimo.EndLoan();
            emprestimo.EndDate.HasValue.Should().BeTrue();
        }
    }
}
