using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Domain.Models;

namespace TestProject.Domain.Controllers.Interfaces
{
	public interface IGameController
	{
		public Task<IEnumerable<Game>> Get(string genreName);

		public Task<Game> Get(Guid id);

		public Task<Game> Insert(Game item);

		public Task<Game> Update(Game item);

		public Task<bool> Delete(Guid id);
	}
}
