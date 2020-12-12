using AutoMapper;
using FluentAssertions;
using Moq;
using MyGame.Application.ApplicationServices;
using MyGame.Application.ApplicationServices.NotificationContext;
using MyGame.Application.Model;
using MyGame.Domain.Entities;
using MyGame.Domain.Repositories;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace MyGames.Unit.Test
{
    public class FriendApplicationServiceTest
    {
        private readonly Mock<IFriendRepository> _friendRepositoryMock;
        private readonly Mock<ILoanRepository> _loanRepositoryMock;
        private readonly Mock<IGameRepository> _gameRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public FriendApplicationServiceTest()
        {
            _friendRepositoryMock = new Mock<IFriendRepository>();
            _loanRepositoryMock = new Mock<ILoanRepository>();
            _gameRepositoryMock = new Mock<IGameRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(x => x.Map<FriendModel>(It.IsAny<Friend>())).Returns(new FriendModel());
        }

        [Fact]
        public async Task DeveCriarAmigo()
        {
            var usuario = new User("1", "teste@teste.com");
            _userRepositoryMock.Setup(x => x.FindByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(usuario));

            var notificationContext = new NotificationContext();
            var service = new FriendApplicationService(notificationContext, _friendRepositoryMock.Object, _mapperMock.Object, _userRepositoryMock.Object
                , _gameRepositoryMock.Object, _loanRepositoryMock.Object);

            await service.CreateAsync(usuario.Id, new FriendModel { Name = "1", Email = "teste@teste.com", Phone = "3333" });

            service.Valid.Should().BeTrue();
        }
        [Fact]
        public async Task NaoDeviCriarAmigoPorFaltadeUsuario()
        {
            var usuario = new User("1", "teste@teste.com");
            _userRepositoryMock.Setup(x => x.FindByIdAsync(It.IsAny<Guid>()));

            var notificationContext = new NotificationContext();
            var service = new FriendApplicationService(notificationContext, _friendRepositoryMock.Object, _mapperMock.Object, _userRepositoryMock.Object
                , _gameRepositoryMock.Object, _loanRepositoryMock.Object);

            await service.CreateAsync(usuario.Id, new FriendModel { Name = "1", Email = "teste@teste.com", Phone = "3333" });

            service.Valid.Should().BeFalse();
            service.GetNotifications.Any(x => x.Message.Contains("Usuário não encontrado")).Should().BeTrue();
        }


        [Fact]
        public async Task NaoDeviCriarPorValidacaoDeEmail()
        {
            var usuario = new User("1", "teste@teste.com");
            _userRepositoryMock.Setup(x => x.FindByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(usuario));

            var notificationContext = new NotificationContext();
            var service = new FriendApplicationService(notificationContext, _friendRepositoryMock.Object, _mapperMock.Object, _userRepositoryMock.Object
                , _gameRepositoryMock.Object, _loanRepositoryMock.Object);

            await service.CreateAsync(usuario.Id, new FriendModel { Name = "1", Email = "", Phone = "3333" });

            service.Valid.Should().BeFalse();
            service.GetNotifications.Any(x => x.Message.Contains("Email invalido")).Should().BeTrue();
        }

        [Fact]
        public async Task DeveEmprestarJogoParaAmigo()
        {
            #region Setup
            var usuario = new User("1", "teste@teste.com");
            _userRepositoryMock.Setup(x => x.FindByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(usuario));

            _friendRepositoryMock.Setup(x => x.FindByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new Friend("1", "teste@teste.com", "333", usuario)));

            _gameRepositoryMock.Setup(x => x.FindByIdAsync(It.IsAny<Guid>()))
              .Returns(Task.FromResult(new Game("1", "1")));

            _loanRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<Loan, bool>>>()))
                    .Returns(Task.FromResult(false));

            var notificationContext = new NotificationContext();
            var service = new FriendApplicationService(notificationContext, _friendRepositoryMock.Object, _mapperMock.Object, _userRepositoryMock.Object
                , _gameRepositoryMock.Object, _loanRepositoryMock.Object);
            #endregion


            await service.Lend(Guid.NewGuid(),Guid.NewGuid());

            service.Valid.Should().BeTrue();
        }

        [Fact]
        public async Task NaoDeveEmprestarJogoAndaEmprestador()
        {
            #region Setup
            var usuario = new User("1", "teste@teste.com");
            _userRepositoryMock.Setup(x => x.FindByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(usuario));

            _friendRepositoryMock.Setup(x => x.FindByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new Friend("1", "teste@teste.com", "333", usuario)));

            _gameRepositoryMock.Setup(x => x.FindByIdAsync(It.IsAny<Guid>()))
              .Returns(Task.FromResult(new Game("1", "1")));

            _loanRepositoryMock.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<Loan, bool>>>()))
                    .Returns(Task.FromResult(true));

            var notificationContext = new NotificationContext();
            var service = new FriendApplicationService(notificationContext, _friendRepositoryMock.Object, _mapperMock.Object, _userRepositoryMock.Object
                , _gameRepositoryMock.Object, _loanRepositoryMock.Object);
            #endregion


            await service.Lend(Guid.NewGuid(), Guid.NewGuid());

            service.Valid.Should().BeFalse();
            service.GetNotifications.Any(x => x.Message.Contains("Este jogo está emprestado. Você deve Encerrar o emprestimo ativo.")).Should().BeTrue();
        }

    }
}
