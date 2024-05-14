using Microsoft.EntityFrameworkCore;
using Repositories.Repositories;
using Server_4.DAL.Models;

namespace Repositories.EntityRepository
{

	public class SemesterRepository : BaseRepository<Semester>
	{
		public SemesterRepository(Java_Console_Auto_GraderContext context) : base(context)
		{
		}


		public async Task<Semester> GetSemesterByIdAsync(int semesterId)
		{
			return (await dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == semesterId))!;
		}

	}

}
