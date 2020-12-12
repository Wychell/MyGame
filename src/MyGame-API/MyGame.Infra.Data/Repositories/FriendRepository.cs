using Microsoft.EntityFrameworkCore;
using MyGame.Domain.Entities;
using MyGame.Domain.Repositories;
using MyGame.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyGame.Infra.Data.Repositories
{
    public class FriendRepository : RepositoryBase<Friend>, IFriendRepository
    {
        public FriendRepository(MyGameContext myGameContext) : base(myGameContext)
        {
        }

        public override async Task<IEnumerable<Friend>> AllAsync()
        {
            var data = await Set.Include(x => x.Loans).ThenInclude(x => x.Game).ToListAsync();
            return data.AsReadOnly();
        }

        public override Task<Friend> FindByIdAsync(Guid id)
        {
            return Set.Include(x => x.Loans).ThenInclude(x => x.Game).FirstAsync(x => x.Id == id);
        }
    }
}
