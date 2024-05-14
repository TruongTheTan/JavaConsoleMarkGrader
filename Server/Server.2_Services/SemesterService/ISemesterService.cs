using Repositories.DTOs;

namespace Services.SemesterService;

public interface ISemesterService
{
	Task<CustomResponse<GetSemesterDTO>> GetSemesterByIdAsync(int semesterId);
	Task<CustomResponse<List<GetSemesterDTO>>> GetListSemesterAsync();
	Task<CustomResponse<dynamic>> CreateNewSemesterAsync(CreateSemesterDTO semesterCreate);
	Task<CustomResponse<dynamic>> UpdateSemesterAsync(UpdateSemesterDTO semesterUpdate);
}

