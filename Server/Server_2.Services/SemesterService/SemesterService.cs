using AutoMapper;
using Repositories;
using Repositories.DTOs;
using Repositories.EntityRepository;
using Server_4.DAL.Models;

namespace Services.SemesterService;

public class SemesterService : ISemesterService
{

	private readonly IMapper mapper;
	private readonly UnitOfWork unitOfWork;
	private readonly SemesterRepository semesterRepository;



	public SemesterService(UnitOfWork unitOfWork, IMapper mapper)
	{
		this.mapper = mapper;
		this.unitOfWork = unitOfWork;
		this.semesterRepository = unitOfWork.SemesterRepository;
	}




	public async Task<CustomResponse<dynamic>> CreateNewSemesterAsync(CreateSemesterDTO semesterDTO)
	{
		CustomResponse<dynamic> customResponse = new();

		Semester semester = new() { SemesterName = semesterDTO.SemesterName };
		bool isCreatedSuccessful = await semesterRepository.CreateAsync(semester);


		if (isCreatedSuccessful)
		{
			customResponse.StatusCode = ServiceUtilities.OK;
			customResponse.Message = "New semester created successfully";
		}
		else
		{
			customResponse.StatusCode = ServiceUtilities.INTERNAL_SERVER_ERROR;
			customResponse.Message = "Fail to create semester";
		}
		return customResponse;
	}





	public async Task<CustomResponse<List<GetSemesterDTO>>> GetListSemesterAsync()
	{
		List<Semester> semesters = await semesterRepository.GetAllAsync();


		CustomResponse<List<GetSemesterDTO>> customResponse = new();

		if (semesters.Count > 0)
		{
			customResponse.StatusCode = ServiceUtilities.OK;
			customResponse.Data = mapper.Map<List<GetSemesterDTO>>(semesters);
		}
		else
		{
			customResponse.StatusCode = ServiceUtilities.NOT_FOUND;
			customResponse.Message = "Semester not found";
		}

		return customResponse;
	}





	public async Task<CustomResponse<GetSemesterDTO>> GetSemesterByIdAsync(int semesterId)
	{
		CustomResponse<GetSemesterDTO> customResponse = new();

		Semester? semester = await semesterRepository.GetByIdAsync(semesterId);

		if (semester == null)
		{
			customResponse.StatusCode = ServiceUtilities.NOT_FOUND;
			customResponse.Message = "Semester not found";
		}
		else
		{
			customResponse.StatusCode = ServiceUtilities.OK;
			customResponse.Message = "Semester found";
			customResponse.Data = mapper.Map<GetSemesterDTO>(semester);
		}
		return customResponse;
	}





	public async Task<CustomResponse<dynamic>> UpdateSemesterAsync(UpdateSemesterDTO semesterUpdateDTO)
	{
		CustomResponse<dynamic> customResponse = new();
		Semester semester = mapper.Map<Semester>(semesterUpdateDTO);


		var semesterFoundById = await semesterRepository.GetByIdAsync(semesterUpdateDTO.Id);

		if (semesterFoundById == null)
		{
			customResponse.StatusCode = ServiceUtilities.NOT_FOUND;
			customResponse.Message = "Semester not found";
		}
		else
		{
			bool isUpdateSuccessful = await semesterRepository.UpdateAsync(semester);

			customResponse.StatusCode = isUpdateSuccessful ? ServiceUtilities.OK : ServiceUtilities.INTERNAL_SERVER_ERROR;
			customResponse.Message = isUpdateSuccessful ? "Semester updated successfully" : "Failed to update semester";
		}
		return customResponse;
	}

}

