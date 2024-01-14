using AutoMapper;
using MarkGrader;
using Microsoft.AspNetCore.Identity;
using Repositories;
using Repositories.DTOs;
using Repositories.EntiyRepository;
using Repositories.Models;

namespace Services.StudentService
{


	public class StudentService : IStudentService
	{

		private readonly IMapper mapper;
		private readonly UnitOfWork unitOfWork;
		private readonly StudentRepository studentRepository;
		private readonly UserManager<IdentityUser> userManager;


		public StudentService(IMapper mapper, UnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
		{
			this.mapper = mapper;
			this.unitOfWork = unitOfWork;
			this.userManager = userManager;
			studentRepository = unitOfWork.StudentRepository;
		}





		public async Task<GetStudentDTO> GetStudentByIdAsync(Guid id)
		{
			IdentityUser studentFound = await userManager.FindByIdAsync(id.ToString());


			if (studentFound != null)
				return mapper.Map<GetStudentDTO>(studentFound);

			return null!;
		}






		public async Task<List<GetStudentSubmissionDetailsDTO>> GetStudentSubmissionHistory(Guid studentId, int semesterId)
		{
			List<StudentSubmissionDetail> studentSubmissionDetails = await studentRepository.GetStudentSubmissionHistory(studentId, semesterId);

			if (studentSubmissionDetails != null)
				return mapper.Map<List<GetStudentSubmissionDetailsDTO>>(studentSubmissionDetails);

			return null!;
		}






		public async Task<GetStudentSubmissionDetailsDTO> GetStudentSubmissionDetails(int SubmissionId)
		{
			StudentSubmissionDetail? studentSubmissionDetail = await studentRepository.GetStudentSubmissionDetails(SubmissionId);


			if (studentSubmissionDetail != null)
				return mapper.Map<GetStudentSubmissionDetailsDTO>(studentSubmissionDetail);

			return null!;
		}






		public async Task<List<GetStudentSubmissionDetailsDTO>> GetListStudentLastSubmissionBySemester(int semesterId)
		{
			List<StudentSubmissionDetail>? studentSubmissionDetails = await studentRepository.GetListStudentSubmissionBySemester(semesterId);


			if (studentSubmissionDetails != null)
			{
				List<GetStudentSubmissionDetailsDTO>? studentSubmissionDetailsDTO = new();


				studentSubmissionDetails.ForEach(submission =>
				{
					// Won't add if a submission alread existed in DTO list
					if (studentSubmissionDetailsDTO.Find(s => s.Id == submission.Id) == null)
					{
						StudentSubmissionDetail studentSubmissionDetail = studentSubmissionDetails.Last(s => s.StudentId.Equals(submission.StudentId));

						studentSubmissionDetailsDTO.Add(mapper.Map<GetStudentSubmissionDetailsDTO>(studentSubmissionDetail));
					}
				});
				return studentSubmissionDetailsDTO;
			}
			return null!;
		}





		public async Task<bool> CreateStudentSubmissionDetailsBySemester(CreateStudentSubmissionDetailsDTO createStudentSubmissionDetailsDTO)
		{
			StudentSubmissionDetail studentSubmissionDetail = mapper.Map<StudentSubmissionDetail>(createStudentSubmissionDetailsDTO);
			return await studentRepository.CreateStudentSubmissionDetails(studentSubmissionDetail);
		}




		public async Task<CreateStudentSubmissionDetailsDTO> GradeStudentMark(int semesterID)
		{
			List<TestCase> testCaseList = await unitOfWork.TestCaseRepository.GetAllTestCaseBySemesterIdAsync(semesterID);

			if (testCaseList.Count > 0)
			{
				StudentMarkGrader.TestCaseList = mapper.Map<List<GetTestCaseDTO>>(testCaseList);
				return StudentMarkGrader.GradeStudentMark();
			}
			return null!;
		}


	}
}
