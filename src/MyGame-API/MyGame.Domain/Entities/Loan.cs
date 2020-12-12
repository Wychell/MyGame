using Flunt.Validations;
using System;

namespace MyGame.Domain.Entities
{
    public class Loan : Entity
    {

        protected Loan()
        {

        }


        public Loan(Guid friendId, Guid gameId)
        {
            FriendId = friendId;
            GameId = gameId;

            AddNotifications(new Contract()
                .Requires()
                .IsNotEmpty(FriendId, "Friend", "Amigo é necessário")
                .IsNotEmpty(GameId, "Game", "O jogo é obrigatório"));
        }

        public Loan(Friend friend, Game game) : this(friend.Id, game.Id)
        {
            Friend = friend;
            Game = game;
        }

        public Guid FriendId { get; private set; }
        public Friend Friend { get; private set; }

        public Guid GameId { get; private set; }
        public Game Game { get; private set; }

        public DateTime? EndDate { get; private set; }

        public void EndLoan()
        {
            EndDate = DateTime.Now;
        }
    }
}
