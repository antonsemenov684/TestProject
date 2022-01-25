using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace TestProject.DB.Models
{
	public partial class Game : BaseEntity
	{
		public Game()
		{
			GameGenreLinks = new HashSet<GameGenreLink>();
		}

		public override Guid Id { get; set; }
		public override string Name { get; set; }
		public Guid DeveloperId { get; set; }

		private Developer _developer;
		public virtual Developer Developer
		{
			get
			{
				if (_developer == null)
				{
					_developer = Db.Entities.Developers.Find(DeveloperId);
				}

				return _developer;
			}
			set
			{
				_developer = value;
			}
		}
		public virtual ICollection<GameGenreLink> GameGenreLinks { get; set; }

		public ICollection<GameGenreLink> GetGameGenreLinks()
		{
			return Db.Entities.GameGenreLinks.Where(item => item.GameId == Id).ToList();
		}
	}
}
