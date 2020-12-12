using AutoMapper;
using MyGame.Application.Model;
using MyGame.Domain.Entities;

namespace MyGame.Application.Mapper
{
    public class DomainToModel : Profile
    {
        public DomainToModel()
        {
            CreateMap<Game, GameModel>();
            CreateMap<Friend, FriendModel>();
            CreateMap<Loan, LoanModel>();
        }
    }
}
