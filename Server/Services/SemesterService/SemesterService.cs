using AutoMapper;
using Repositories;
using Repositories.DTOs;
using Repositories.Models;

namespace Services.SemesterService
{
	public class SemesterService : ISemesterService
	{
		private readonly IMapper mapper;
		private readonly UnitOfWork unitOfWork;

		public SemesterService(UnitOfWork unitOfWork, IMapper mapper)
		{
			this.mapper = mapper;
			this.unitOfWork = unitOfWork;
		}



		public async Task<bool> CreateNewSemesterAsync(CreateSemesterDTO semesterDTO)
		{
			Semester semester = new() { SemesterName = semesterDTO.SemesterName };

			return await unitOfWork.SemesterRepository.CreateNewSemesterAsync(semester);
		}



		public async Task<GetSemesterDTO> GetSemesterByIdAsync(int semesterId)
		{
			Semester semester = await unitOfWork.SemesterRepository.GetSemesterByIdAsync(semesterId);

			return mapper.Map<GetSemesterDTO>(semester);
		}



		public async Task<bool> UpdateSemesterAsync(UpdateSemesterDTO semesterUpdateDTO)
		{
			Semester semester = mapper.Map<Semester>(semesterUpdateDTO);

			return await unitOfWork.SemesterRepository.UpdateSemesterAsync(semester);
		}
	}
}
