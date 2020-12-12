using AutoMapper;
using FluentAssertions;
using Moq;
using MyGame.Application.ApplicationServices;
using MyGame.Application.ApplicationServices.NotificationContext;
using MyGame.Application.Model;
using MyGame.Domain.Entities;
using MyGame.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyGames.Unit.Test
{
    public class GameApplicationServiceTest
    {

        private readonly Mock<IGameRepository> _gameRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        public GameApplicationServiceTest()
        {
            _mapperMock = new Mock<IMapper>();
            _gameRepositoryMock = new Mock<IGameRepository>();
            _mapperMock.Setup(x => x.Map<GameModel>(It.IsAny<Game>())).Returns(new GameModel());
        }


        [Fact]
        public async Task DeveAtualizarMock()
        {
            _gameRepositoryMock.Setup(x => x.FindByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new Game("1", "1")));


            var notificationContext = new NotificationContext();
            var gameService = new GameApplicationService(notificationContext, _gameRepositoryMock.Object, _mapperMock.Object);


            await gameService.UpdateAsync(Guid.NewGuid(), new GameModel {Name ="1", Gender = "1" });

            gameService.Valid.Should().BeTrue();
            gameService.GetNotifications.Count.Should().Be(0);

        }

        [Fact]
        public async Task NaoDeveLocalizarParaAtualizar()
        {
            var notificationContext = new NotificationContext();
            var gameService = new GameApplicationService(notificationContext, _gameRepositoryMock.Object, _mapperMock.Object);

            await gameService.UpdateAsync(Guid.NewGuid(), new GameModel());

            gameService.Valid.Should().BeFalse();
            gameService.GetNotifications.Any(x => x.Message.Contains("")).Should().BeTrue();

        }
    }
}
