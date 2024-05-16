using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Repositories;
using Repositories.DTOs;
using Repositories.EntityRepository;
using Server.DAL.Entities;
using Server_4.DAL.Models;

namespace Services.StudentService;



public partial class StudentService
{

	private readonly IMapper mapper;
	private readonly UnitOfWork unitOfWork;
	private readonly StudentRepository studentRepository;
	private readonly UserManager<AppUser> userManager;


	public StudentService(IMapper mapper, UnitOfWork unitOfWork, UserManager<AppUser> userManager)
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



	public async Task<bool> CreateStudentSubmissionDetailsBySemester(CreateStudentSubmissionDetailsDTO createStudentSubmissionDetailsDTO)
	{
		StudentSubmissionDetail studentSubmissionDetail = mapper.Map<StudentSubmissionDetail>(createStudentSubmissionDetailsDTO);
		return await studentRepository.CreateStudentSubmissionDetails(studentSubmissionDetail);
	}

}

