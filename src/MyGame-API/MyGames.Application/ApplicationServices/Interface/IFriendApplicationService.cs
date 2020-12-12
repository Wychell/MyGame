using MyGame.Application.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyGame.Application.ApplicationServices.Interface
{
    public interface IFriendApplicationService
    {
        Task<IEnumerable<FriendModel>> AllAsync();
        Task<FriendModel> CreateAsync(Guid userId, FriendModel model);
        Task DeleteAsync(Guid id);
        Task<FriendModel> GetAsync(Guid id);
        Task<FriendModel> Lend(Guid friendId, Guid gameId);
        Task<FriendModel> Finalizeloan(Guid friendId, Guid loandId);
        Task<FriendModel> RevogeLoan(Guid friendId);
        Task<FriendModel> UpdateAsync(Guid id, FriendModel model);
    }
}
