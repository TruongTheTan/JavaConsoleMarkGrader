using Microsoft.EntityFrameworkCore;
using Repositories.Repositories;
using Server_4.DAL.Models;

namespace Repositories.EntityRepository
{

	public class TestCaseRepository : BaseRepository<TestCase>
	{
		public TestCaseRepository(JavaConsoleAutoGraderContext context) : base(context)
		{
		}


		public override async Task<List<TestCase>> GetAllAsync()
		{
			return await dbSet.Include(t => t.Semester).AsNoTracking().ToListAsync();
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
