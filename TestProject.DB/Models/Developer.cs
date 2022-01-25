using System;
using System.Collections.Generic;

#nullable disable

namespace TestProject.DB.Models
{
	public partial class Developer : BaseEntity
	{
		public Developer()
		{
			Games = new HashSet<Game>();
		}

		public override Guid Id { get; set; }
		public override string Name { get; set; }

		public virtual ICollection<Game> Games { get; set; }
	}
}
