using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.DB.Models
{
	public abstract class BaseEntity
	{
		public abstract Guid Id { get; set; }
		public abstract string Name { get; set; }
	}
}
