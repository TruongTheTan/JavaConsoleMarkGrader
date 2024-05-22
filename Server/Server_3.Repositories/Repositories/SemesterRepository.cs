using Repositories.Repositories;
using Server_4.DAL.Models;

namespace Repositories.EntityRepository
{

	public class SemesterRepository : BaseRepository<Semester>
	{
		public SemesterRepository(JavaConsoleAutoGraderContext context) : base(context)
		{
		}

	}

}
