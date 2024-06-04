using System.Net;
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
		await semesterRepository.CreateAsync(semester);


		bool isCreatedSuccessful = await unitOfWork.SaveChangesAsync();


		if (isCreatedSuccessful)
		{
			customResponse.StatusCode = (int)HttpStatusCode.OK;
			customResponse.Message = "New semester created successfully";
		}
		else
		{
			customResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
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
			customResponse.StatusCode = (int)HttpStatusCode.OK;
			customResponse.Data = mapper.Map<List<GetSemesterDTO>>(semesters);
		}
		else
		{
			customResponse.StatusCode = (int)HttpStatusCode.NotFound;
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
			customResponse.StatusCode = (int)HttpStatusCode.NotFound;
			customResponse.Message = "Semester not found";
		}
		else
		{
			customResponse.StatusCode = (int)HttpStatusCode.OK;
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
			customResponse.StatusCode = (int)HttpStatusCode.NotFound;
			customResponse.Message = "Semester not found";
		}
		else
		{

			semesterRepository.Update(semester);

			bool isUpdateSuccessful = await unitOfWork.SaveChangesAsync();

			customResponse.StatusCode = isUpdateSuccessful ? (int)HttpStatusCode.OK : (int)HttpStatusCode.InternalServerError;
			customResponse.Message = isUpdateSuccessful ? "Semester updated successfully" : "Failed to update semester";
		}
		return customResponse;
	}

}

