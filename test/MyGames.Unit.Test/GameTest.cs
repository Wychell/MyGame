using FluentAssertions;
using MyGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MyGames.Unit.Test
{
    public class GameTest
    {
        [Fact]
        public void DeveCriarJogo()
        {
            var jogo = new Game("GTA V", "Aventura");

            jogo.Valid.Should().BeTrue();
        }

        [Fact]
        public void DeveValidarGenero()
        {
            var jogo = new Game("GT V", "");

            jogo.Valid.Should().BeFalse();
            jogo.Notifications.Any(x => x.Message.Contains("Gênero é obrigatório")).Should().BeTrue();
        }

        [Fact]
        public void DeveAtualizarJogo()
        {
            var jogo = new Game("GT V", "Aventura");

            jogo.Update("GTA IV", "Luta");

            jogo.Name.Should().Be("GTA IV");
            jogo.Gender.Should().Be("Luta");
            jogo.Valid.Should().BeTrue();
        }
    }
}
