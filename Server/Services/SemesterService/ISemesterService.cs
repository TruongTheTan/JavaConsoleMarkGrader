using Repositories.DTOs.Semester;

namespace Services.SemesterService
{
    public interface ISemesterService
	{
		Task<GetSemesterDTO> GetSemesterByIdAsync(int semesterId);
		Task<bool> CreateNewSemesterAsync(CreateSemesterDTO semesterCreate);
		Task<bool> UpdateSemesterAsync(UpdateSemesterDTO semesterUpdate);
	}
}
