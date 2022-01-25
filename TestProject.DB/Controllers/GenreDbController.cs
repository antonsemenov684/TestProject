using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestProject.DB.Controllers.Interfaces;
using TestProject.DB.Models;

namespace TestProject.DB.Controllers
{
	public class GenreDbController : DbController<Genre>, IGenreDbController
	{
		protected override DbSet<Genre> Entities => Db.Entities.Genres;
	}
}
