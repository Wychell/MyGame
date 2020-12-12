using System;
using System.Collections.Generic;

namespace MyGame.Application.Model
{
    public class FriendModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public IList<LoanModel> Loans { get; set; }
    }
}
