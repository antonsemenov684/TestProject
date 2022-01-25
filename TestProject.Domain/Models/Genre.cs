using System;

namespace TestProject.Domain.Models
{
	public class Genre
	{
		public string ContentType { get; set; } = "GenreType";
		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}
