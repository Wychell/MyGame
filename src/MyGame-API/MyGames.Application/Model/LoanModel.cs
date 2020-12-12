using System;

namespace MyGame.Application.Model
{
    public class LoanModel
    {
        public Guid? Id { get; set; }
        public Guid GameId { get;  set; }
        public Guid FriendId { get;  set; }
        public GameModel Game { get; set; }
        public DateTime? EndDate { get; private set; }
    }
}
