using Flunt.Validations;
using System.Collections.Generic;
using System.Linq;

namespace MyGame.Domain.Entities
{
    public class User : Entity
    {
        private ICollection<Friend> _friends = new HashSet<Friend>();

        protected User()
        {

        }
        public User(string name, string email)
        {
            Name = name;
            Email = email;
            AddNotifications(new Contract()
                      .Requires()
                      .IsNotNullOrEmpty(Name, "Nome", "Nome é obrigatório")
                      .IsEmail(Email, "Email", "Email invalido"));
        }

        public User(string name, string email, string password) : this(name, email)
        {
            Password = password;
        }


        public string Name { get; private set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public IReadOnlyCollection<Friend> Friends => (IReadOnlyCollection<Friend>)_friends;


        public void AddFriend(Friend friend)
        {
            if (!_friends.Any(x => x == friend))
                _friends.Add(friend);
            else
                AddNotification("Friend", "Este amigo já existe!");

        }

    }
}
