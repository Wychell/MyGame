using Microsoft.EntityFrameworkCore;
using MyGame.Domain.Entities;
using MyGame.Domain.Repositories;
using MyGame.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyGame.Infra.Data.Repositories
{
    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        public GameRepository(MyGameContext myGameContext) : base(myGameContext)
        {
        }

        public override async Task<IEnumerable<Game>> AllAsync()
        {
            var data = await Set.Include(x => x.Loans).ThenInclude(x => x.Friend).ToListAsync();
            return data.AsReadOnly();
        }

        public override Task<Game> FindByIdAsync(Guid id)
        {
            return Set.Include(x => x.Loans).ThenInclude(x => x.Friend).FirstAsync(x => x.Id == id);
        }
    }
}
