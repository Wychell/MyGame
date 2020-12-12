using Microsoft.AspNetCore.Mvc;
using MyGame.Application.ApplicationServices.Interface;
using MyGame.Application.Model;
using MyGame.Infra.Security;
using MyGames.Api.ViewModel;
using System;
using System.Threading.Tasks;

namespace MyGames.Api.Controlles
{
    [Route("api/[controller]")]
    public class FriendController : BaseController
    {
        private readonly IFriendApplicationService _friendApplicationService;
        private readonly IAuthUserResolver _authUserResolver;

        public FriendController(IFriendApplicationService friendApplicationService, IAuthUserResolver authUserResolver)
        {
            _friendApplicationService = friendApplicationService;
            _authUserResolver = authUserResolver;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _friendApplicationService.AllAsync();
            var result = MakeResult(data);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _friendApplicationService.GetAsync(id);
            var result = MakeResult(data);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FriendModel model)
        {
            var userId = _authUserResolver.GetUserId();
            var data = await _friendApplicationService.CreateAsync(userId, model);
            var result = MakeResult(data);
            return  Ok(result);
        }

        [HttpPost("{id:guid}/lend")]
        public async Task<IActionResult> Lend([FromRoute]Guid id, [FromBody] LeanViewModel model)
        {
            var data = await _friendApplicationService.Lend(id, model.GameId.GetValueOrDefault());
            var result = MakeResult(data);
            return Ok(result);
        }

        [HttpPut("{id:guid}/lend/finalize")]
        public async Task<IActionResult> Finalize([FromRoute] Guid id, [FromBody] LeanViewModel model)
        {
            var data = await _friendApplicationService.Finalizeloan(id, model.LoanId.GetValueOrDefault());
            var result = MakeResult(data);
            return Ok(result);
        }


        [HttpPost("{id:guid}/revoge-all-loan")]
        public async Task<IActionResult> Revoge([FromRoute] Guid id)
        {
            var data = await _friendApplicationService.RevogeLoan(id);
            var result = MakeResult(data);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id,[FromBody]FriendModel model)
        {
            var data = await _friendApplicationService.UpdateAsync(id,model);
            var result = MakeResult(data);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _friendApplicationService.DeleteAsync(id);
            return Ok(MakeResult(true));
        }
    }
}
