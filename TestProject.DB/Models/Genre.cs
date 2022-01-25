using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace TestProject.DB.Models
{
	public partial class Genre : BaseEntity
	{
		public Genre()
		{
			GameGenreLinks = new HashSet<GameGenreLink>();
		}

		public override Guid Id { get; set; }
		public override string Name { get; set; }

		public virtual ICollection<GameGenreLink> GameGenreLinks { get; set; }
	}
}
