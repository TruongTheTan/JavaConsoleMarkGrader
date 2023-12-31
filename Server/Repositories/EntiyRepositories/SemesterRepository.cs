using Microsoft.EntityFrameworkCore;
using Repositories.Models;

namespace Repositories.EntiyRepository
{
	public class SemesterRepository : BaseRepository<Semester>
	{
		public SemesterRepository(PRO192_Auto_GraderContext context) : base(context)
		{
		}


		public async Task<Semester> GetSemesterByIdAsync(int semesterId)
		{
			return (await dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == semesterId))!;
		}


		public async Task<bool> CreateNewSemesterAsync(Semester semester)
		{
			return await CreateAsync(semester);
		}


		public async Task<bool> UpdateSemesterAsync(Semester semester)
		{
			return await UpdateAsync(semester);
		}
	}
}
