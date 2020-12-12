using MyGame.Application.Model;
using System.Threading.Tasks;

namespace MyGame.Application.ApplicationServices.Interface
{
    public interface IUsuarioApplicationService
    {
        Task<UsuarioModel> LoginAsync(string login, string password);
    }
}
