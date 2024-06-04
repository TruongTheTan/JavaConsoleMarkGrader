using Repositories.EntityRepository;
using Server_4.DAL.Models;

namespace Repositories;

public sealed class UnitOfWork
{

	private UserRepository? userRepository;
	private StudentRepository? studentRepository;
	private SemesterRepository? semesterRepository;
	private TestCaseRepository? testCaseRepository;
	private readonly JavaConsoleAutoGraderContext context;

	public UnitOfWork(JavaConsoleAutoGraderContext context)
	{
		this.context = context;
	}

	public JavaConsoleAutoGraderContext Context => this.context;
	public UserRepository UserRepository => this.userRepository ??= new UserRepository(context);
	public TestCaseRepository TestCaseRepository => testCaseRepository ??= new TestCaseRepository(context);
	public StudentRepository StudentRepository => this.studentRepository ??= new StudentRepository(context);
	public SemesterRepository SemesterRepository => this.semesterRepository ??= new SemesterRepository(context);



	public async Task<bool> SaveChangesAsync()
	{
		return (await context.SaveChangesAsync() > 0);
	}
}

