using Repositories.DTOs;

namespace Services.TestCaseService
{
	public interface ITestCaseService
	{
		Task<List<GetTestCaseDTO>> GetAllTestCaseBySemesterIdAsync(int semesterId);
		Task<GetTestCaseDTO> GetTestCaseByIdAsync(int testCaseId);
		Task<bool> CreateNewTestCaseAsync(CreateTestCaseDTO testCase);
		Task<bool> UpdateTestCaseAsync(UpdateTestCaseDTO testCase);
	}
}
