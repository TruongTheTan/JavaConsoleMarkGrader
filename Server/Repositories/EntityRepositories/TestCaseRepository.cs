using Microsoft.EntityFrameworkCore;
using Repositories.Models;

namespace Repositories.EntiyRepository
{

	public class TestCaseRepository : BaseRepository<TestCase>
	{
		public TestCaseRepository(Java_Console_Auto_GraderContext context) : base(context)
		{
		}



		public async Task<List<TestCase>> GetAllTestCaseBySemesterIdAsync(int semesterId)
		{
			return await dbSet.Include(t => t.Semester).Where(t => t.SemesterId == semesterId).AsNoTracking().ToListAsync(); ;
		}



		public async Task<TestCase?> GetTestCaseByIdAsync(int testCaseId)
		{
			return await dbSet.AsNoTracking().FirstOrDefaultAsync(t => t.Id == testCaseId);
		}

	}

}
