using Microsoft.EntityFrameworkCore;
using Repositories.Models;

namespace Repositories.EntiyRepository
{

	public class StudentRepository : BaseRepository<User>
	{

		private readonly DbSet<StudentSubmissionDetail> studentSubmissionDetailTable;


		public StudentRepository(PRO192_Auto_GraderContext context) : base(context)
		{
			studentSubmissionDetailTable = context.StudentSubmissionDetails;
		}


		public async Task<User?> FindStudentByIdAsync(Guid id)
		{
			return await dbSet
				.Where(student => student.Id.Equals(id) && student.RoleId == 2)
				.AsNoTracking()
				.FirstOrDefaultAsync();
		}



		public async Task<List<StudentSubmissionDetail>> GetStudentSubmissionHistory(Guid studentId, int semesterId)
		{
			return await studentSubmissionDetailTable
				.Where(e => e.StudentId.Equals(studentId) && e.SemesterId == semesterId)
				.AsNoTracking()
				.ToListAsync();
		}




		public async Task<StudentSubmissionDetail> GetStudentLastSubmission(Guid studentId, int semesterId)
		{
			return (await studentSubmissionDetailTable
				.Where(e => e.StudentId.Equals(studentId) && e.SemesterId == semesterId)
				.AsNoTracking()
				.ToListAsync())
				.Last();
		}



		public async Task<StudentSubmissionDetail?> GetStudentSubmissionDetails(int submissionId)
		{
			return await studentSubmissionDetailTable.AsNoTracking().FirstOrDefaultAsync(e => e.Id == submissionId);
		}




		public async Task<List<StudentSubmissionDetail>?> GetListStudentSubmissionBySemester(int semesterId)
		{
			return await studentSubmissionDetailTable
				.Where(s => s.SemesterId == semesterId)
				.AsNoTracking()
				.ToListAsync();
		}


		public async Task<bool> CreateStudentSubmissionDetails(StudentSubmissionDetail studentSubmissionDetail)
		{
			await studentSubmissionDetailTable.AddAsync(studentSubmissionDetail);
			return await SaveChangesAsync();
		}
	}
}
