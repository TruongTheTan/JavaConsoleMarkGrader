using MarkGrader.Helpers;
using Microsoft.AspNetCore.Mvc;
using Repositories.DTOs;
using Services.SemesterService;


namespace MarkGrader.Controllers;


[Route("api/[controller]")]
[ApiController]
//[Authorize(Roles = "Admin")]
public class SemesterController : ControllerBase
{

	private readonly ISemesterService semesterService;



	public SemesterController(ISemesterService semesterService)
	{
		this.semesterService = semesterService;
	}




	[HttpGet("list")]
	public async Task<IActionResult> GetSemesterList()
	{
		CustomResponse<List<GetSemesterDTO>> customResponse = await semesterService.GetListSemesterAsync();
		return HttpsUtility.ReturnActionResult(customResponse);
	}




	[HttpGet]
	public async Task<IActionResult> GetSemesterById([FromQuery] int semesterId)
	{
		CustomResponse<GetSemesterDTO> customResponse = await semesterService.GetSemesterByIdAsync(semesterId);
		return HttpsUtility.ReturnActionResult(customResponse);
	}




	[HttpPatch]
	public async Task<IActionResult> UpdateSemester([FromBody] UpdateSemesterDTO updateSemesterDTO)
	{
		CustomResponse<dynamic> customResponse = await semesterService.UpdateSemesterAsync(updateSemesterDTO);
		return HttpsUtility.ReturnActionResult(customResponse);
	}
}

