using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestProject.DB.Controllers;
using TestProject.DB.Controllers.Interfaces;
using TestProject.DB.Models;

namespace TestProject.DB
{
	public interface IGameController
	{
		public Task<IEnumerable<Game>> GetAll();

		public Task<Game> Get(Guid id);

		public Task<IEnumerable<Game>> Get(string genreName);

		public Task<Game> Insert(Game item);

		public Task<Game> Update(Game item);

		public Task<bool> Delete(Guid id);
	}
}
