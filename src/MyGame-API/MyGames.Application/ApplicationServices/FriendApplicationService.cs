using AutoMapper;
using MyGame.Application.ApplicationServices.Interface;
using MyGame.Application.ApplicationServices.NotificationContext;
using MyGame.Application.Model;
using MyGame.Domain.Entities;
using MyGame.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Application.ApplicationServices
{
    public class FriendApplicationService : ApplicationServiceBase<Friend>, IFriendApplicationService
    {
        private readonly IFriendRepository _friendRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public FriendApplicationService(INotificationContext notificationContext,
            IFriendRepository friendRepository,
            IMapper mapper,
            IUserRepository userRepository,
            IGameRepository gameRepository,
            ILoanRepository loanRepository) : base(notificationContext)
        {
            _friendRepository = friendRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _gameRepository = gameRepository;
            _loanRepository = loanRepository;
        }

        public async Task<IEnumerable<FriendModel>> AllAsync()
        {
            var data = await _friendRepository.AllAsync();
            return _mapper.Map<IEnumerable<FriendModel>>(data);
        }

        public async Task<FriendModel> CreateAsync(Guid userId, FriendModel model)
        {
            var user = await _userRepository.FindByIdAsync(userId);

            if (user == null)
            {
                AddNotification("User", "Usuário não encontrado");
                return default;
            }

            var entity = new Friend(model.Name, model.Email, model.Phone, user);


            if (Validate(entity))
            {
                await _friendRepository.InsertAsync(entity);
                await _friendRepository.CommitAsync();
            }

            return _mapper.Map<FriendModel>(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _friendRepository.DeleteAsync(id);
            await _friendRepository.CommitAsync();
        }

        public async Task<FriendModel> Lend(Guid friendId, Guid gameId)
        {
            var friend = await _friendRepository.FindByIdAsync(friendId);
            var game = await _gameRepository.FindByIdAsync(gameId);

            if (friend == null)
                AddNotification("Friend", "Amigo não encontrado");

            if (game == null)
                AddNotification("Game", "Jogo não encontrado");

            bool isGameBorrowed = await _loanRepository.AnyAsync(x => x.GameId == gameId && !x.EndDate.HasValue);

            if (isGameBorrowed)
                AddNotification("Game", "Este jogo está emprestado. Você deve Encerrar o emprestimo ativo.");

            if (!Valid)
                return default;

            var loan = new Loan(friend, game);
            friend.AddLoan(loan);

            if (Validate<Entity>(friend, loan))
            {
                await _loanRepository.InsertAsync(loan);
                await _friendRepository.UpdateAsync(friend);

                await _friendRepository.CommitAsync();
            }

            return _mapper.Map<FriendModel>(friend);
        }

        public async Task<FriendModel> RevogeLoan(Guid friendId)
        {
            var friend = await _friendRepository.FindByIdAsync(friendId);
            if (friend == null)
            {
                AddNotification("Friend", "Amigo não encontrado");
                return default;
            }

            friend.RevokeAllLoan();


            if (Validate(friend))
            {
                await _friendRepository.UpdateAsync(friend);
                await _friendRepository.CommitAsync();
            }

            return _mapper.Map<FriendModel>(friend);
        }

        public async Task<FriendModel> GetAsync(Guid id)
        {
            var result = await _friendRepository.FindByIdAsync(id);
            return _mapper.Map<FriendModel>(result);
        }

        public async Task<FriendModel> UpdateAsync(Guid id, FriendModel model)
        {
            var entity = await _friendRepository.FindByIdAsync(id);

            if (entity == null)
            {
                AddNotification("Friend", "Amigo não encontrado");
                return default;
            }

            entity.Update(model.Name, model.Email, model.Phone);

            if (Validate(entity))
            {
                await _friendRepository.UpdateAsync(entity);
                await _friendRepository.CommitAsync();
            }

            return _mapper.Map<FriendModel>(entity);
        }

        public async Task<FriendModel> Finalizeloan(Guid friend, Guid loandId)
        {
            var entity = await _friendRepository.FindByIdAsync(friend);

            if (entity == null)
            {
                AddNotification("Friend", "Amigo não encontrado");
                return default;
            }

            entity.FinalizeLoan(loandId);

            if (Validate(entity))
            {
                await _friendRepository.UpdateAsync(entity);
                await _friendRepository.CommitAsync();
            }

            return _mapper.Map<FriendModel>(entity);
        }
    }
}
