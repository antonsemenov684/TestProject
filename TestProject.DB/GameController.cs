using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestProject.DB.Controllers;
using TestProject.DB.Controllers.Interfaces;
using TestProject.DB.Models;

namespace TestProject.DB
{
	public class GameController : IGameController
	{
		private IGameDbController Games { get; }
		private IDeveloperDbController Developers { get; }
		private IGenreDbController Genres { get; }

		public GameController(IGameDbController games, IDeveloperDbController developers, IGenreDbController genres)
		{
			Games = games;
			Developers = developers;
			Genres = genres;
		}

		public async Task<IEnumerable<Game>> GetAll()
		{
			return await Games.GetAll();
		}

		public async Task<Game> Get(Guid id)
		{
			return await Games.Get(id);
		}

		public async Task<IEnumerable<Game>> Get(string genreName)
		{
			var games = from game in Db.Entities.Games
						join genreLink in Db.Entities.GameGenreLinks on game.Id equals genreLink.GameId
						where genreLink.Genre.Name == genreName
						select game;
			/*var games2 = Db.Entities.Genres
				.Where(item => item.Name == genreName)
				.SelectMany(item => item.GameGenreLinks)
				.Select(item => item.Game);*/
			return await games.ToListAsync();
		}

		public async Task<Game> Insert(Game item)
		{
			if (item.Id != Guid.Empty)
			{
				throw new ArgumentException("Game cannot contain Id when inserting.");
			}

			ValidateGame(item);

			var developer = await Developers.Get(item.Developer.Name) ?? await Developers.Insert(item.Developer);
			var game = await Games.Insert(new Game
			{
				Name = item.Name,
				DeveloperId = developer.Id,
			});

			var genres = item.GameGenreLinks
				.Select(link => link.Genre);

			foreach (var genre in genres)
			{
				var genreDb = await Genres.Get(genre.Name) ?? await Genres.Insert(genre);
				await Db.Entities.GameGenreLinks.AddAsync(new GameGenreLink
				{
					Id = Guid.NewGuid(),
					GameId = game.Id,
					GenreId = genreDb.Id,
				});
			}

			await Db.Entities.SaveChangesAsync();
			return game;
		}

		public async Task<Game> Update(Game item)
		{
			if (item.Id == Guid.Empty)
			{
				throw new ArgumentException("Game must contain Id when updating.");
			}

			ValidateGame(item);

			var game = await Games.Get(item.Id);
			if (game == null)
			{
				throw new ArgumentException("Game must contain correct Id when updating.");
			}

			var developer = await Developers.Get(item.Developer.Name) ?? await Developers.Insert(item.Developer);
			game.DeveloperId = developer.Id;

			var genres = item.GameGenreLinks
				.Select(link => link.Genre);

			var linksInDb = game.GetGameGenreLinks().ToList();
			var linksAdded = new List<GameGenreLink>();

			foreach (var genre in genres)
			{
				var genreDb = await Genres.Get(genre.Name) ?? await Genres.Insert(genre);
				var link = await Db.Entities.GameGenreLinks.Where(link => link.GameId == game.Id && link.GenreId == genreDb.Id).SingleOrDefaultAsync();
				link ??= (await Db.Entities.GameGenreLinks.AddAsync(new GameGenreLink
				{
					Id = Guid.NewGuid(),
					GameId = game.Id,
					GenreId = genreDb.Id,
				})).Entity;
				linksAdded.Add(link);
			}

			foreach (var linkInDb in linksInDb)
			{
				if (!linksAdded.Contains(linkInDb))
				{
					Db.Entities.GameGenreLinks.Remove(linkInDb);
				}
			}

			return await Games.Update(game);
		}

		public async Task<bool> Delete(Guid id)
		{
			var links = await Db.Entities.GameGenreLinks.Where(item => item.GameId == id).ToArrayAsync();
			Db.Entities.GameGenreLinks.RemoveRange(links);

			return await Games.Delete(id);
		}

		private void ValidateGame(Game item)
		{

			if (item.Developer == null || string.IsNullOrWhiteSpace(item.Developer.Name))
			{
				throw new ArgumentException("Game must contain Developer.");
			}

			if (item.GameGenreLinks == null)
			{
				throw new ArgumentException("Genres cannot be null.");
			}

			item.GameGenreLinks = item.GameGenreLinks.Where(link => link.Genre != null && !string.IsNullOrWhiteSpace(link.Genre.Name)).ToList();

			if (item.GameGenreLinks.Count <= 0)
			{
				throw new ArgumentException("Game must contain Genres.");
			}
		}
	}
}
