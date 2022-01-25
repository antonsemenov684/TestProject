using System;
using System.Collections.Generic;

namespace TestProject.Domain.Models
{
	public class Game
	{
		public string ContentType { get; set; } = "GameType";
		public Guid Id { get; set; }
		public string Name { get; set; }
		public Developer Developer { get; set; }
		public IEnumerable<Genre> Genres { get; set; }
	}
}
