using AutoMapper;
using MyGame.Application.ApplicationServices.Interface;
using MyGame.Application.ApplicationServices.NotificationContext;
using MyGame.Application.Model;
using MyGame.Domain.Entities;
using MyGame.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Application.ApplicationServices
{
    public class GameApplicationService : ApplicationServiceBase<Game>, IGameApplicationService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        public GameApplicationService(INotificationContext notificationContext,
            IGameRepository gameRepository,
            IMapper mapper) : base(notificationContext)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }


        public async Task<GameModel> CreateAsync(GameModel gameModel)
        {
            var entity = new Game(gameModel.Name, gameModel.Gender);

            if (Validate(entity))
            {
                await _gameRepository.InsertAsync(entity);
                await _gameRepository.CommitAsync();
            }

            return _mapper.Map<GameModel>(entity);
        }

        public async Task<GameModel> GetAsync(Guid id)
        {
            var entity = await _gameRepository.FindByIdAsync(id);
            return _mapper.Map<GameModel>(entity);
        }

        public async Task<IEnumerable<GameModel>> AllAsync()
        {
            var entity = await _gameRepository.AllAsync();
            return _mapper.Map<IEnumerable<GameModel>>(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _gameRepository.DeleteAsync(id);
            await _gameRepository.CommitAsync();
        }

        public async Task<GameModel> UpdateAsync(Guid id, GameModel gameModel)
        {
            var entity = await _gameRepository.FindByIdAsync(id);

            if (entity == null)
            {
                AddNotification("Game", "Jogo não encontrado");
                return default;
            }

            entity.Update(gameModel.Name, gameModel.Gender);

            if (Validate(entity))
            {
                await _gameRepository.UpdateAsync(entity);
                await _gameRepository.CommitAsync();
            }

            return _mapper.Map<GameModel>(entity);
        }

    }
}
