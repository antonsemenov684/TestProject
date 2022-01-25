using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Domain.Controllers.Interfaces;
using TestProject.Domain.Models;

namespace TestProject.View.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class GameController : ControllerBase
	{
		private readonly ILogger<GameController> _logger;
		private readonly IGameController _gameController;

		public GameController(ILogger<GameController> logger, IGameController gameController)
		{
			_logger = logger;
			_gameController = gameController;
		}

		[HttpPost]
		public async Task<Game> Create(Game game)
		{
			if (game == null)
			{
				throw new ArgumentNullException("Game cannot be null.");
			}

			if (game.ContentType != "GameType")
			{
				throw new ArgumentException("ContentType must be GameType");
			}

			return await _gameController.Insert(game);
		}

		[HttpGet]
		public async Task<IEnumerable<Game>> Read(string genre)
		{
			return await _gameController.Get(genre);
		}

		[HttpGet("{id}")]
		public async Task<Game> Read(Guid id)
		{
			if (id == Guid.Empty)
			{
				throw new ArgumentException("Incorrect Id");
			}

			return await _gameController.Get(id);
		}

		[HttpPut]
		public async Task<Game> Update(Game game)
		{
			if (game == null)
			{
				throw new ArgumentNullException("Game cannot be null.");
			}

			if (game.ContentType != "GameType")
			{
				throw new ArgumentException("ContentType must be GameType");
			}

			return await _gameController.Update(game);
		}

		[HttpDelete]
		public async Task<bool> Delete(Game game)
		{
			if (game == null)
			{
				throw new ArgumentNullException("Game cannot be null.");
			}

			if (game.ContentType != "GameType")
			{
				throw new ArgumentException("ContentType must be GameType");
			}

			return await _gameController.Delete(game.Id);
		}
	}
}
