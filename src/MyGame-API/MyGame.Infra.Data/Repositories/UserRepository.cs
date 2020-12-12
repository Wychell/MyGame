using MyGame.Domain.Entities;
using MyGame.Domain.Repositories;
using MyGame.Infra.Data.Context;

namespace MyGame.Infra.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(MyGameContext myGameContext) : base(myGameContext)
        {
        }
    }
}
