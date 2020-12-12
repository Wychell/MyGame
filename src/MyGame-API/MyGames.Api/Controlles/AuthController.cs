using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyGame.Application.ApplicationServices.Interface;
using MyGames.Api.ViewModel;
using System.Threading.Tasks;

namespace MyGames.Api.Controlles
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IUsuarioApplicationService usuarioApplicationService;

        public AuthController(IUsuarioApplicationService usuarioApplicationService)
        {
            this.usuarioApplicationService = usuarioApplicationService;
        }


        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AuthModel model)
        {
            var data = await usuarioApplicationService.LoginAsync(model.login, model.password);
            var result = MakeResult(data);
            return Ok(result);
        }
    }
}
