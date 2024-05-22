using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.DAL.Entities;

namespace Server_4.DAL.Contexts;

public class MigrationDBContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
	public MigrationDBContext(DbContextOptions options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
	}
}
