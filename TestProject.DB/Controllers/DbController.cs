using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestProject.DB.Models;

namespace TestProject.DB.Controllers
{
	public abstract class DbController<T> : IDbController<T> where T : BaseEntity
	{
		protected abstract DbSet<T> Entities { get; }

		public virtual async Task<IEnumerable<T>> GetAll()
		{
			return await Entities.ToListAsync();
		}

		public virtual async Task<T> Get(Guid id)
		{
			return await Entities.Where(item => item.Id == id).SingleOrDefaultAsync();
		}

		public virtual async Task<T> Get(string name)
		{
			return await Entities.Where(item => item.Name == name).FirstOrDefaultAsync();
		}

		public virtual async Task<T> Insert(T item)
		{
			item.Id = Guid.NewGuid();
			await Entities.AddAsync(item);
			await Db.Entities.SaveChangesAsync();
			return item;
		}

		public virtual async Task<T> Update(T item)
		{
			Entities.Update(item);
			await Db.Entities.SaveChangesAsync();
			return item;
		}

		public virtual async Task<bool> Delete(Guid id)
		{
			var item = await Get(id);
			Entities.Remove(item);
			return await Db.Entities.SaveChangesAsync() > 0;
		}
	}
}
