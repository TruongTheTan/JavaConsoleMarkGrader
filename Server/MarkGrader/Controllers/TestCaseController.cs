using Microsoft.AspNetCore.Mvc;
using Repositories.DTOs;
using Services.TestCaseService;

namespace MarkGrader.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestCaseController : ControllerBase
	{
		private readonly ITestCaseService testService;



		public TestCaseController(ITestCaseService testService)
		{
			this.testService = testService;
		}





		[HttpGet]
		//[Authorize(Policy = "AdminAndTeacherRole")]
		public async Task<IActionResult> GetTestCaseById([FromQuery] int testCaseId)
		{
			GetTestCaseDTO getTestCaseDTO = await this.testService.GetTestCaseByIdAsync(testCaseId);


			if (getTestCaseDTO == null)
				return NotFound("No test case found");

			return Ok(getTestCaseDTO);
		}






		[HttpGet("list")]
		public async Task<IActionResult> GetAllTestCaseBySemester([FromQuery] int semesterId)
		{
			List<GetTestCaseDTO> testCaseDTOList = await this.testService.GetAllTestCaseBySemesterIdAsync(semesterId);


			if (testCaseDTOList == null)
				return Problem();

			if (testCaseDTOList.Count <= 0)
				return NotFound("No test case found");

			return Ok(testCaseDTOList);
		}








		[HttpPost("create")]
		public async Task<IActionResult> CreateTestCase([FromBody] CreateTestCaseDTO testCase)
		{
			if (ModelState.IsValid && testCase.CheckValidInputType == true)
			{
				bool createSuccessfull = await this.testService.CreateNewTestCaseAsync(testCase);

				if (!createSuccessfull)
					return Problem("Unable to add new test case, check again");

				return Created(nameof(CreateTestCase), testCase);
			}
			return BadRequest();
		}




		[HttpPut("update")]
		public async Task<IActionResult> UpdateTestCase([FromBody] UpdateTestCaseDTO testCase)
		{
			if (ModelState.IsValid)
			{
				bool updateSuccessfull = await this.testService.UpdateTestCaseAsync(testCase);

				if (!updateSuccessfull)
					return Problem("Unable to update test case, check again");

				return Ok("Test case updated successfully");
			}
			return BadRequest();
		}

	}

}
