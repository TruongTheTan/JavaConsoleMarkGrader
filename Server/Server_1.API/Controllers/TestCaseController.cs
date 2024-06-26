﻿using MarkGrader.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.DTOs;
using Services.TestCaseService;

namespace MarkGrader.Controllers;



[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class TestCaseController : ControllerBase
{

	private readonly TestCaseService testCaseService;



	public TestCaseController(TestCaseService testCaseService)
	{
		this.testCaseService = testCaseService;
	}




	[HttpGet]
	public async Task<IActionResult> GetTestCaseById([FromQuery] int testCaseId)
	{
		CustomResponse<GetTestCaseDTO> customResponse = await testCaseService.GetTestCaseByIdAsync(testCaseId);
		return HttpsUtility.ReturnActionResult(customResponse);
	}



	[HttpGet("list/by-semester-id")]
	public async Task<IActionResult> GetAllTestCaseBySemester([FromQuery] int semesterId)
	{
		CustomResponse<List<GetTestCaseDTO>> customResponse = await testCaseService.GetAllTestCaseBySemesterIdAsync(semesterId);
		return HttpsUtility.ReturnActionResult(customResponse);
	}




	[HttpGet("list")]
	public async Task<IActionResult> GetAllTestCase()
	{
		CustomResponse<List<GetTestCaseDTO>> customResponse = await testCaseService.GetAllTestCaseAsync();
		return HttpsUtility.ReturnActionResult(customResponse);
	}





	[HttpPost("create")]
	public async Task<IActionResult> CreateTestCase([FromBody] CreateTestCaseDTO createTestCaseDTO)
	{
		CustomResponse<dynamic> customResponse = await testCaseService.CreateNewTestCaseAsync(createTestCaseDTO);
		return HttpsUtility.ReturnActionResult(customResponse);
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


