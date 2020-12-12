using MyGame.Application.ApplicationServices.Interface;
using MyGame.Application.ApplicationServices.NotificationContext;
using MyGame.Application.Model;
using MyGame.Domain.Entities;
using MyGame.Domain.Repositories;
using MyGame.Infra.Security;
using System.Threading.Tasks;

namespace MyGame.Application.ApplicationServices
{
    public class UsuarioApplicationService : ApplicationServiceBase<User> , IUsuarioApplicationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenProvider _jwtTokenProvider;
        public UsuarioApplicationService(INotificationContext notificationContext, IUserRepository userRepository, IJwtTokenProvider jwtTokenProvider) : base(notificationContext)
        {
            _userRepository = userRepository;
            _jwtTokenProvider = jwtTokenProvider;
        }

        public async Task<UsuarioModel> LoginAsync(string login, string password)
        {
          var user = await  _userRepository.FindAsync(x => x.Email == login && x.Password == password);

            if(user == null)
            {
                AddNotification("Login", "Login ou Senha inválidos");
                return default;
            }

            var token =_jwtTokenProvider.GenerateToken(user);
            return new UsuarioModel { Email = user.Email, Name = user.Name, Token = token };
        }
    }
}
