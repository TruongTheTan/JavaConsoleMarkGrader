using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Repositories;
using Repositories.DTOs;
using Repositories.Models;

namespace Services.StudentService
{
	public class StudentCreateService : StudentService
	{
		public StudentCreateService(IMapper mapper, UnitOfWork unitOfWork, UserManager<IdentityUser> userManager) : base(mapper, unitOfWork, userManager)
		{
		}



		public async Task<bool> CreateStudentSubmissionDetailsBySemester(CreateStudentSubmissionDetailsDTO createStudentSubmissionDetailsDTO)
		{
			StudentSubmissionDetail studentSubmissionDetail = mapper.Map<StudentSubmissionDetail>(createStudentSubmissionDetailsDTO);
			return await studentRepository.CreateStudentSubmissionDetails(studentSubmissionDetail);
		}
	}
}
