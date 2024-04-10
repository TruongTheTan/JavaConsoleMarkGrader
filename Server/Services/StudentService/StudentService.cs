using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Repositories;
using Repositories.DTOs;
using Repositories.EntiyRepository;
using Repositories.Models;

namespace Services.StudentService
{


	public class StudentService
	{

		protected readonly IMapper mapper;
		protected readonly UnitOfWork unitOfWork;
		protected readonly StudentRepository studentRepository;
		protected readonly UserManager<IdentityUser> userManager;


		public StudentService(IMapper mapper, UnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
		{
			this.mapper = mapper;
			this.unitOfWork = unitOfWork;
			this.userManager = userManager;
			studentRepository = unitOfWork.StudentRepository;
		}




		public async Task<CreateStudentSubmissionDetailsDTO> GradeStudentMark(int semesterID)
		{
			List<TestCase> testCaseList = await unitOfWork.TestCaseRepository.GetAllTestCaseBySemesterIdAsync(semesterID);

			if (testCaseList.Count > 0)
			{
				StudentMarkGraderService.TestCaseList = mapper.Map<List<GetTestCaseDTO>>(testCaseList);
				return StudentMarkGraderService.GradeStudentMark();
			}
			return null!;
		}


	}
}
