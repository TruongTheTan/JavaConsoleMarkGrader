using System.Net;
using Repositories.DTOs;
using Server.DAL.Entities;
using Server_4.DAL.Models;

namespace Services.StudentService;

public partial class StudentService
{




	public async Task<GetStudentDTO> GetStudentByIdAsync(Guid id)
	{
		AppUser studentFound = await userManager.FindByIdAsync(id.ToString());


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






	public async Task<CustomResponse<GetStudentSubmissionDetailsDTO>> GetStudentSubmissionDetails(int submissionId)
	{
		CustomResponse<GetStudentSubmissionDetailsDTO> customResponse = new();
		StudentSubmissionDetail? studentSubmissionDetail = await studentRepository.GetStudentSubmissionDetails(submissionId);


		if (studentSubmissionDetail is not null)
		{
			customResponse.StatusCode = (int)HttpStatusCode.OK;
			customResponse.Message = "Submission found";
			customResponse.Data = mapper.Map<GetStudentSubmissionDetailsDTO>(studentSubmissionDetail);
		}
		return customResponse;
	}






	public async Task<List<GetStudentSubmissionDetailsDTO>> GetListStudentLastSubmissionBySemester(int semesterId)
	{
		List<StudentSubmissionDetail>? studentSubmissionDetails = await studentRepository.GetListStudentSubmissionBySemester(semesterId);


		if (studentSubmissionDetails != null)
		{
			List<GetStudentSubmissionDetailsDTO>? studentSubmissionDetailsDTO = new();


			studentSubmissionDetails.ForEach(submission =>
			{
				// Won't add if a submission already existed in DTO list
				if (studentSubmissionDetailsDTO.Find(s => s.Id == submission.Id) == null)
				{
					StudentSubmissionDetail studentSubmissionDetail = studentSubmissionDetails.Last(s => s.StudentId!.Equals(submission.StudentId));

					studentSubmissionDetailsDTO.Add(mapper.Map<GetStudentSubmissionDetailsDTO>(studentSubmissionDetail));
				}
			});
			return studentSubmissionDetailsDTO;
		}
		return null!;
	}
}

