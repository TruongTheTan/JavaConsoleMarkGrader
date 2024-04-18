using AutoMapper;
using Repositories;
using Repositories.DTOs;
using Repositories.EntiyRepository;
using Repositories.Models;

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
		CustomResponse<List<GetTestCaseDTO>> customResponse = new();
		List<TestCase> testCaseList = await testCaseRepository.GetAllTestCaseBySemesterIdAsync(semesterId);


		if (testCaseList != null && testCaseList?.Count > 0)
		{
			customResponse.Message = "Test cases found";
			customResponse.StatusCode = ServiceUtilities.OK;
			customResponse.Data = mapper.Map<List<GetTestCaseDTO>>(testCaseList);
		}
		else
		{
			customResponse.Message = "Test cases not found";
			customResponse.StatusCode = ServiceUtilities.NOT_FOUND;
		}
		return customResponse;
	}



	public async Task<CustomResponse<GetTestCaseDTO>> GetTestCaseByIdAsync(int testCaseId)
	{
		TestCase? testCase = await testCaseRepository.GetTestCaseByIdAsync(testCaseId);

		CustomResponse<GetTestCaseDTO> customResponse = new();

		if (testCase != null)
		{
			customResponse.Message = "Test case found";
			customResponse.StatusCode = ServiceUtilities.OK;
			customResponse.Data = mapper.Map<GetTestCaseDTO>(testCase);
		}
		else
		{
			customResponse.Message = "Test case not found";
			customResponse.StatusCode = ServiceUtilities.NOT_FOUND;
		}

		return customResponse;
	}





	public async Task<CustomResponse<dynamic>> CreateNewTestCaseAsync(CreateTestCaseDTO testCase)
	{

		CustomResponse<dynamic> customResponse = new();

		TestCase newTestCase = mapper.Map<TestCase>(testCase);
		bool isCreateSuccessful = await testCaseRepository.CreateAsync(newTestCase);


		if (isCreateSuccessful)
		{
			customResponse.StatusCode = ServiceUtilities.CREATED;
			customResponse.Message = "New test case created successfully";
		}
		else
		{
			customResponse.StatusCode = ServiceUtilities.INTERNAL_SERVER_ERROR;
			customResponse.Message = "Fail to create new test case";
		}
		return customResponse;
	}






	public async Task<CustomResponse<dynamic>> UpdateTestCaseAsync(UpdateTestCaseDTO testCaseUpdateDTO)
	{
		CustomResponse<dynamic> customResponse = new();

		TestCase testCaseToUpdate = mapper.Map<TestCase>(testCaseUpdateDTO);

		bool isUpdateSuccess = await testCaseRepository.UpdateAsync(testCaseToUpdate);

		if (isUpdateSuccess)
		{
			customResponse.StatusCode = ServiceUtilities.OK;
			customResponse.Message = "Test case updated successfully";
		}
		else
		{
			customResponse.StatusCode = ServiceUtilities.INTERNAL_SERVER_ERROR;
			customResponse.Message = "Fail to update test case";
		}
		return customResponse;
	}

}


