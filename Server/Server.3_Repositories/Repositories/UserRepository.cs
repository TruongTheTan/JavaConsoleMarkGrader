using Repositories.Repositories;
using Server_4.DAL.Models;

namespace Repositories.EntityRepository
{

	public class UserRepository : BaseRepository<AspNetUser>
	{

		public UserRepository(JavaConsoleAutoGraderContext context) : base(context)
		{
		}

	}

}
