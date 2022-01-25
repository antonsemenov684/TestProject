using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Domain.Controllers.Interfaces;
using TestProject.Domain.Models;
using IDbGameController = TestProject.DB.IGameController;
using DbGameController = TestProject.DB.GameController;

namespace TestProject.Domain.Controllers
{
	public class GameController : IGameController
	{
		private readonly IDbGameController _gameController;

		public GameController()
		{
			_gameController = new DbGameController();
		}

		public async Task<IEnumerable<Game>> Get(string genreName)
		{
			if (string.IsNullOrWhiteSpace(genreName))
			{
				return (await _gameController.GetAll()).Select(item => item.ToDomainModel());
			}

			return (await _gameController.Get(genreName)).Select(item => item.ToDomainModel());
		}

		public async Task<Game> Get(Guid id)
		{
			return (await _gameController.Get(id))?.ToDomainModel();
		}

		public async Task<Game> Insert(Game item)
		{
			var game = await _gameController.Insert(item.ToDbModel());
			return game.ToDomainModel();
		}

		public async Task<Game> Update(Game item)
		{
			var game = await _gameController.Update(item.ToDbModel());
			return game.ToDomainModel();
		}

		public async Task<bool> Delete(Guid id)
		{
			return await _gameController.Delete(id);
		}
	}
}
