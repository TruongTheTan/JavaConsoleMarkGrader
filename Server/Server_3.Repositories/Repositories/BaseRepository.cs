﻿using Microsoft.EntityFrameworkCore;
using Server_4.DAL.Models;

namespace Repositories.Repositories;

public abstract class BaseRepository<T> where T : class
{

	protected DbSet<T> dbSet;
	protected JavaConsoleAutoGraderContext context;



	protected BaseRepository(JavaConsoleAutoGraderContext context)
	{
		this.context = context;
		dbSet = context.Set<T>();
	}



	public async Task<T?> GetByIdAsync(object id)
	{
		return await dbSet.FindAsync(id);
	}


	public virtual async Task<List<T>> GetAllAsync()
	{
		return await dbSet.AsNoTracking().ToListAsync();
	}


	public async Task CreateAsync(T entity)
	{
		await dbSet.AddAsync(entity);
	}


	public void Update(T entity)
	{
		dbSet.Update(entity);
	}


	protected async Task<bool> SaveChangesAsync()
	{
		return await context.SaveChangesAsync() > 0;
	}

}

