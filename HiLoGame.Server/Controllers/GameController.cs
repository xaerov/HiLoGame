using HiLoGame.Application.Services.Interfaces;
using HiLoGame.Domain;
using HiLoGame.Server.Requests;
using Microsoft.AspNetCore.Mvc;

namespace HiLoGame.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Game>> CreateGame([FromBody] CreateGameRequest request)
        {
            var game = await _gameService.CreateGameAsync(request.PlayerName, request.Min, request.Max);
            return Ok(game);
        }

        [HttpPost("join")]
        public async Task<ActionResult<Game>> JoinGame([FromBody] JoinGameRequest request)
        {
            var game = await _gameService.JoinGameAsync(request.GameId, request.PlayerName);
            return Ok(game);
        }

        [HttpPost("guess")]
        public async Task<ActionResult<string>> MakeGuess([FromBody] MakeGuessRequest request)
        {
            var result = await _gameService.MakeGuessAsync(request.GameId, request.Guess, request.PlayerName);
            return Ok(result);
        }
    }
}
