using Microsoft.AspNetCore.Mvc;
using MyGame.Application.ApplicationServices.Interface;
using MyGame.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGames.Api.Controlles
{
    [Route("api/[controller]")]
    public class GameController : BaseController
    {
        private readonly IGameApplicationService _gameApplicationService;

        public GameController(IGameApplicationService gameApplicationService)
        {
            _gameApplicationService = gameApplicationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _gameApplicationService.AllAsync();
            var result = MakeResult(data);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GameModel model)
        {
            var data = await _gameApplicationService.CreateAsync(model);
            var result = MakeResult(data);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _gameApplicationService.GetAsync(id);
            var result = MakeResult(data);
            return Ok(result);
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] GameModel model)
        {
            var data = await _gameApplicationService.UpdateAsync(id, model);
            var result = MakeResult(data);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _gameApplicationService.DeleteAsync(id);
            return Ok(MakeResult(true));
        }
    }
}
