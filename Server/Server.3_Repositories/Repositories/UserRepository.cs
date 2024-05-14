using Repositories.Repositories;
using Server_4.DAL.Models;

namespace Repositories.EntityRepository
{

	public class UserRepository : BaseRepository<AspNetUser>
	{

		public UserRepository(Java_Console_Auto_GraderContext context) : base(context)
		{
		}

	}

}
