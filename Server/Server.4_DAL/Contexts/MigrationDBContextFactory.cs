using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Server_4.DAL.Contexts
{
	internal class MigrationDBContextFactory : IDesignTimeDbContextFactory<MigrationDBContext>
	{
		public MigrationDBContext CreateDbContext(string[] args)
		{
			const string connectionString = "server=(local);database=HelloThere;Trusted_Connection=True;uid=sa;pwd=sa;";

			var optionBuilder = new DbContextOptionsBuilder<MigrationDBContext>();
			optionBuilder.UseSqlServer(connectionString);

			return new MigrationDBContext(optionBuilder.Options);
		}
	}
}
