using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject;

namespace TestProject.Domain
{
	internal static class ConvertExtentions
	{
		public static Models.Game ToDomainModel(this DB.Models.Game dbGame)
		{
			var game = new Models.Game
			{
				Id = dbGame.Id,
				Name = dbGame.Name,
				Developer = new Models.Developer
				{
					Id = dbGame.Developer.Id,
					Name = dbGame.Developer.Name,
				},
				Genres = dbGame.GetGameGenreLinks().Select(link => new Models.Genre
				{
					Id = link.Genre.Id,
					Name = link.Genre.Name,
				}).ToList(),
			};

			return game;
		}

		public static DB.Models.Game ToDbModel(this Models.Game game)
		{
			var dbGame = new DB.Models.Game
			{
				Id = game.Id,
				Name = game.Name,
				Developer = new DB.Models.Developer
				{
					Name = game.Developer.Name,
				},
				GameGenreLinks = game.Genres.Select(genre => new DB.Models.GameGenreLink()
				{
					Genre = new DB.Models.Genre
					{
						Name = genre.Name,
					}
				}).ToList(),
			};

			return dbGame;
		}
	}
}
