using System;
using System.Collections.Generic;

#nullable disable

namespace TestProject.DB.Models
{
	public partial class GameGenreLink
	{
		public Guid Id { get; set; }
		public Guid GameId { get; set; }
		public Guid GenreId { get; set; }

		private Game _game;
		public virtual Game Game
		{
			get
			{
				if (_game == null)
				{
					_game = Db.Entities.Games.Find(GameId);
				}

				return _game;
			}
			set
			{
				_game = value;
			}
		}

		private Genre _genre;
		public virtual Genre Genre
		{
			get
			{
				if (_genre == null)
				{
					_genre = Db.Entities.Genres.Find(GenreId);
				}

				return _genre;
			}
			set
			{
				_genre = value;
			}
		}
	}
}
