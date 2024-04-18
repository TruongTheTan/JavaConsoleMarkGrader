using Microsoft.AspNetCore.Identity;
using Repositories.DTOs;
using Repositories.Models;

namespace Services.StudentService;

public partial class StudentService
{




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
					StudentSubmissionDetail studentSubmissionDetail = studentSubmissionDetails.Last(s => s.StudentId!.Equals(submission.StudentId));

					studentSubmissionDetailsDTO.Add(mapper.Map<GetStudentSubmissionDetailsDTO>(studentSubmissionDetail));
				}
			});
			return studentSubmissionDetailsDTO;
		}
		return null!;
	}
}

