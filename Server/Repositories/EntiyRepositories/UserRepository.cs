using Microsoft.EntityFrameworkCore;
using Repositories.Models;

namespace Repositories.EntiyRepository
{

	public class UserRepository : BaseRepository<User>
	{
		public UserRepository(PRO192_Auto_GraderContext context) : base(context)
		{
		}



		public async Task<User> GetUserByEmailAndPassword(string email, string password)
		{
			return (await dbSet
				.Where(user => user.Email!.Equals(email) && user.Password!.Equals(password))
				.Include(user => user.Role)
				.AsNoTracking()
				.FirstOrDefaultAsync())!;



		}


		public async Task<User> GetUserByEmail(string email)
		{
			return (await dbSet
				.Where(user => user.Email!.Equals(email)).Include(user => user.Role).AsNoTracking().FirstOrDefaultAsync())!;
		}
	}

}
