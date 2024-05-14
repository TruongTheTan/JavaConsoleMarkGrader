using Microsoft.EntityFrameworkCore;
using Server_4.DAL.Models;

namespace Repositories.Repositories;

public abstract class BaseRepository<T> where T : class
{

	protected DbSet<T> dbSet;
	protected Java_Console_Auto_GraderContext context;



	protected BaseRepository(Java_Console_Auto_GraderContext context)
	{
		this.context = context;
		dbSet = context.Set<T>();
	}


	public async Task<List<T>> GetAllAsync()
	{
		return await dbSet.AsNoTracking().ToListAsync();
	}


	public async Task<bool> CreateAsync(T entity)
	{
		await dbSet.AddAsync(entity);
		return await SaveChangesAsync();
	}


	public async Task<bool> UpdateAsync(T entity)
	{
		dbSet.Update(entity);
		return await SaveChangesAsync();
	}


	protected async Task<bool> SaveChangesAsync()
	{
		return await context.SaveChangesAsync() > 0;
	}

}

