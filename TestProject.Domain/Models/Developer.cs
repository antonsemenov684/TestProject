using System;

namespace TestProject.Domain.Models
{
	public class Developer
	{
		public string ContentType { get; set; } = "DeveloperType";
		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}
