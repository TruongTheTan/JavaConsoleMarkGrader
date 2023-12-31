using Microsoft.EntityFrameworkCore;
using Repositories.Models;

namespace Repositories
{
	public abstract class BaseRepository<T> where T : class
	{
		protected DbSet<T> dbSet;
		protected PRO192_Auto_GraderContext context;

		protected BaseRepository(PRO192_Auto_GraderContext context)
		{
			this.context = context;
			dbSet = context.Set<T>();
		}


		protected async Task<List<T>> GetAllAsync()
		{
			return await dbSet.AsNoTracking().ToListAsync();
		}


		protected async Task<bool> CreateAsync(T entity)
		{
			await dbSet.AddAsync(entity);
			return await this.SaveChangesAsync();
		}


		protected async Task<bool> UpdateAsync(T entity)
		{
			dbSet.Update(entity);
			bool saveChangesSucessfull = await this.SaveChangesAsync();
			this.context.Entry(entity).State = EntityState.Detached;
			return saveChangesSucessfull;
		}


		protected async Task<bool> SaveChangesAsync()
		{
			return await this.context.SaveChangesAsync() > 0;
		}
	}
}
