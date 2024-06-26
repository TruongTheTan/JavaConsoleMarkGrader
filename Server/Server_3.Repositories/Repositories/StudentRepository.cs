﻿using Microsoft.EntityFrameworkCore;
using Repositories.Repositories;
using Server.DAL.Entities;
using Server_4.DAL.Models;

namespace Repositories.EntityRepository;



public class StudentRepository : BaseRepository<AppUser>
{

	private readonly DbSet<StudentSubmissionDetail> studentSubmissionDetailTable;


	public StudentRepository(JavaConsoleAutoGraderContext context) : base(context)
	{

		studentSubmissionDetailTable = context.StudentSubmissionDetails;
	}




	public async Task<List<StudentSubmissionDetail>> GetStudentSubmissionHistory(Guid studentId, int semesterId)
	{
		return await studentSubmissionDetailTable
			.Where(e => e.StudentId!.Equals(studentId) && e.SemesterId == semesterId)
			.AsNoTracking()
			.ToListAsync();
	}




	public async Task<StudentSubmissionDetail> GetStudentLastSubmission(Guid studentId, int semesterId)
	{
		return (await studentSubmissionDetailTable
			.Where(e => e.StudentId!.Equals(studentId) && e.SemesterId == semesterId)
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
		return await studentSubmissionDetailTable.Where(s => s.SemesterId == semesterId).AsNoTracking().ToListAsync();
	}



	public async Task<bool> CreateStudentSubmissionDetails(StudentSubmissionDetail studentSubmissionDetail)
	{
		await studentSubmissionDetailTable.AddAsync(studentSubmissionDetail);
		return await SaveChangesAsync();
	}

}



