using Repositories.DTOs;

namespace Services.StudentService
{
	public interface IStudentService
	{
		//Task<List<GetStudentDTO>?> GetListStudentAsync();
		Task<GetStudentDTO>? GetStudentByIdAsync(Guid id);
		Task<GetStudentSubmissionDetailsDTO> GetStudentSubmissionDetails(int submissionId);
		Task<List<GetStudentSubmissionDetailsDTO>> GetListStudentLastSubmissionBySemester(int semesterId);
		Task<List<GetStudentSubmissionDetailsDTO>> GetStudentSubmissionHistory(Guid studentId, int semesterId);
		Task<bool> CreateStudentSubmissionDetailsBySemester(CreateStudentSubmissionDetailsDTO createStudentSubmissionDetailsDTO);
		Task<CreateStudentSubmissionDetailsDTO> GradeStudentMark(int semester);
	}
}
