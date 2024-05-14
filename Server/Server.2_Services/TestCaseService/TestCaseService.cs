using AutoMapper;
using Repositories;
using Repositories.DTOs;
using Repositories.EntityRepository;
using Server_4.DAL.Models;

namespace Services.TestCaseService;

public class TestCaseService
{


	private readonly IMapper mapper;
	private readonly UnitOfWork unitOfWork;
	private readonly TestCaseRepository testCaseRepository;



	public TestCaseService(IMapper mapper, UnitOfWork unitOfWork)
	{
		this.mapper = mapper;
		this.unitOfWork = unitOfWork;
		this.testCaseRepository = unitOfWork.TestCaseRepository;
	}




	public async Task<CustomResponse<List<GetTestCaseDTO>>> GetAllTestCaseBySemesterIdAsync(int semesterId)
	{

		List<TestCase> testCaseList = await testCaseRepository.GetAllTestCaseBySemesterIdAsync(semesterId);

		bool testCaseAvailable = testCaseList != null && testCaseList?.Count > 0;


		return new CustomResponse<List<GetTestCaseDTO>>()
		{
			Message = testCaseAvailable ? "Test cases found" : "Test cases not found",
			Data = testCaseAvailable ? mapper.Map<List<GetTestCaseDTO>>(testCaseList) : null,
			StatusCode = testCaseAvailable ? ServiceUtilities.OK : ServiceUtilities.NOT_FOUND,
		};

	}



	public async Task<CustomResponse<GetTestCaseDTO>> GetTestCaseByIdAsync(int testCaseId)
	{
		TestCase? testCase = await testCaseRepository.GetTestCaseByIdAsync(testCaseId);

		bool isTestCaseNotNull = testCase != null;

		return new CustomResponse<GetTestCaseDTO>
		{
			StatusCode = isTestCaseNotNull ? ServiceUtilities.OK : ServiceUtilities.NOT_FOUND,
			Message = isTestCaseNotNull ? "Test case found" : "Test case not found",
			Data = isTestCaseNotNull ? mapper.Map<GetTestCaseDTO>(testCase) : null
		};
	}





	public async Task<CustomResponse<dynamic>> CreateNewTestCaseAsync(CreateTestCaseDTO testCase)
	{

		TestCase newTestCase = mapper.Map<TestCase>(testCase);
		bool isCreateSuccessful = await testCaseRepository.CreateAsync(newTestCase);


		return new CustomResponse<dynamic>()
		{
			StatusCode = isCreateSuccessful ? ServiceUtilities.CREATED : ServiceUtilities.INTERNAL_SERVER_ERROR,
			Message = isCreateSuccessful ? "New test case created successfully" : "Fail to create new test case"
		};
	}






	public async Task<CustomResponse<dynamic>> UpdateTestCaseAsync(UpdateTestCaseDTO testCaseUpdateDTO)
	{

		TestCase testCaseToUpdate = mapper.Map<TestCase>(testCaseUpdateDTO);
		bool isUpdateSuccess = await testCaseRepository.UpdateAsync(testCaseToUpdate);


		return new CustomResponse<dynamic>()
		{
			StatusCode = isUpdateSuccess ? ServiceUtilities.OK : ServiceUtilities.INTERNAL_SERVER_ERROR,
			Message = isUpdateSuccess ? "Test case updated successfully" : "Fail to update test case"
		};
	}

}


