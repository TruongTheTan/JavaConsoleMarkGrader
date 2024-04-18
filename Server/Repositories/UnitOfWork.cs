using Repositories.EntityRepository;
using Repositories.EntiyRepository;
using Repositories.Models;

namespace Repositories;

public sealed class UnitOfWork
{

	private UserRepository? userRepository;
	private StudentRepository? studentRepository;
	private SemesterRepository? semesterRepository;
	private TestCaseRepository? testCaseRepository;
	private readonly Java_Console_Auto_GraderContext context;



	public Java_Console_Auto_GraderContext Context => context;
	public UserRepository UserRepository { get => this.userRepository ??= new UserRepository(context); }
	public TestCaseRepository TestCaseRepository { get => testCaseRepository ??= new TestCaseRepository(context); }
	public StudentRepository StudentRepository { get => this.studentRepository ??= new StudentRepository(context); }
	public SemesterRepository SemesterRepository { get => this.semesterRepository ??= new SemesterRepository(context); }


	public UnitOfWork(Java_Console_Auto_GraderContext context)
	{
		this.context = context;
	}

}

