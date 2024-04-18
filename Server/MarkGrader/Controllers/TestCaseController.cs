using MarkGrader.Helpers;
using Microsoft.AspNetCore.Mvc;
using Repositories.DTOs;
using Services.TestCaseService;

namespace MarkGrader.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestCaseController : ControllerBase
{

	private readonly TestCaseService testCaseService;



	public TestCaseController(TestCaseService testCaseService)
	{
		this.testCaseService = testCaseService;
	}




	[HttpGet]
	//[Authorize(Policy = "AdminAndTeacherRole")]
	public async Task<IActionResult> GetTestCaseById([FromQuery] int testCaseId)
	{
		CustomResponse<GetTestCaseDTO> customResponse = await testCaseService.GetTestCaseByIdAsync(testCaseId);
		return HttpsUtility.ReturnActionResult(customResponse);
	}






	[HttpGet("list")]
	public async Task<IActionResult> GetAllTestCaseBySemester([FromQuery] int semesterId)
	{
		CustomResponse<List<GetTestCaseDTO>> customResponse = await testCaseService.GetAllTestCaseBySemesterIdAsync(semesterId);
		return HttpsUtility.ReturnActionResult(customResponse);
	}








	[HttpPost("create")]
	public async Task<IActionResult> CreateTestCase([FromBody] CreateTestCaseDTO testCase)
	{
		if (ModelState.IsValid && testCase.CheckValidInputType)
		{
			CustomResponse<dynamic> customResponse = await testCaseService.CreateNewTestCaseAsync(testCase);
			return HttpsUtility.ReturnActionResult(customResponse);
		}
		return BadRequest(ModelState);
	}




	[HttpPut("update")]
	public async Task<IActionResult> UpdateTestCase([FromBody] UpdateTestCaseDTO testCase)
	{
		if (ModelState.IsValid)
		{
			CustomResponse<dynamic> customResponse = await testCaseService.UpdateTestCaseAsync(testCase);
			return HttpsUtility.ReturnActionResult(customResponse);
		}
		return BadRequest(ModelState);
	}

}


