using MyGame.Domain.Entities;
using MyGame.Domain.Repositories;
using MyGame.Infra.Data.Context;

namespace MyGame.Infra.Data.Repositories
{
    public class LoanRepository : RepositoryBase<Loan>, ILoanRepository
    {
        public LoanRepository(MyGameContext myGameContext) : base(myGameContext)
        {
        }
    }
}
