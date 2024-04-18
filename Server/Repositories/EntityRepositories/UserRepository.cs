using Repositories.Models;

namespace Repositories.EntiyRepository
{

	public class UserRepository : BaseRepository<AspNetUser>
	{

		public UserRepository(Java_Console_Auto_GraderContext context) : base(context)
		{
		}

	}

}
