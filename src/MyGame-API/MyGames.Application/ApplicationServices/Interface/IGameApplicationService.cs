using MyGame.Application.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyGame.Application.ApplicationServices.Interface
{
    public interface IGameApplicationService
    {
        Task<IEnumerable<GameModel>> AllAsync();
        Task<GameModel> CreateAsync(GameModel model);
        Task DeleteAsync(Guid id);
        Task<GameModel> GetAsync(Guid id);
        Task<GameModel> UpdateAsync(Guid id, GameModel model);
    }
}
