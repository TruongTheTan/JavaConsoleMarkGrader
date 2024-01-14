using AutoMapper;
using Repositories;
using Repositories.DTOs;
using Repositories.EntiyRepository;
using Repositories.Models;

namespace Services.SemesterService
{
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



		public async Task<bool> CreateNewSemesterAsync(CreateSemesterDTO semesterDTO)
		{
			Semester semester = new() { SemesterName = semesterDTO.SemesterName };

			return await semesterRepository.CreateAsync(semester);
		}



		public async Task<GetSemesterDTO> GetSemesterByIdAsync(int semesterId)
		{
			Semester semester = await semesterRepository.GetSemesterByIdAsync(semesterId);

			return mapper.Map<GetSemesterDTO>(semester);
		}



		public async Task<bool> UpdateSemesterAsync(UpdateSemesterDTO semesterUpdateDTO)
		{
			Semester semester = mapper.Map<Semester>(semesterUpdateDTO);

			return await semesterRepository.UpdateAsync(semester);
		}

	}
}
