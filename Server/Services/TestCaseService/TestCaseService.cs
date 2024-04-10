using AutoMapper;
using Repositories;
using Repositories.DTOs;
using Repositories.EntiyRepository;
using Repositories.Models;

namespace Services.TestCaseService
{
	public partial class TestCaseService : ITestCaseService
	{


		private readonly IMapper mapper;
		private readonly UnitOfWork unitOfWork;
		private readonly TestCaseRepository testCaseRepository;



		public TestCaseService(IMapper mapper, UnitOfWork unitOfWork)
		{
			this.mapper = mapper;
			this.unitOfWork = unitOfWork;
			testCaseRepository = unitOfWork.TestCaseRepository;
		}



		public async Task<bool> CreateNewTestCaseAsync(CreateTestCaseDTO testCase)
		{
			TestCase newTestCase = mapper.Map<TestCase>(testCase);
			return await testCaseRepository.CreateAsync(newTestCase);
		}



		public async Task<List<GetTestCaseDTO>> GetAllTestCaseBySemesterIdAsync(int semesterId)
		{
			List<TestCase> testCaseList = await testCaseRepository.GetAllTestCaseBySemesterIdAsync(semesterId);

			if (testCaseList != null || testCaseList?.Count > 0)
				return mapper.Map<List<GetTestCaseDTO>>(testCaseList);

			return null!;
		}



		public async Task<GetTestCaseDTO> GetTestCaseByIdAsync(int testCaseId)
		{
			TestCase? testCase = await testCaseRepository.GetTestCaseByIdAsync(testCaseId);


			if (testCase != null)
				return mapper.Map<GetTestCaseDTO>(testCase);

			return null!;
		}




		public async Task<bool> UpdateTestCaseAsync(UpdateTestCaseDTO testCaseUpdateDTO)
		{
			TestCase testCaseToUpdate = mapper.Map<TestCase>(testCaseUpdateDTO);

			return await testCaseRepository.UpdateAsync(testCaseToUpdate);
		}

	}

}
