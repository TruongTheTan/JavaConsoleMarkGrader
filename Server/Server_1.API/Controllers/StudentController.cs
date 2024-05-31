using System.ComponentModel.DataAnnotations;
using MarkGrader.Helpers;
using Microsoft.AspNetCore.Mvc;
using Repositories.DTOs;
using Services.StudentService;

namespace Server_1.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
	private readonly StudentService studentService;


	public StudentController(StudentService studentService)
	{
		this.studentService = studentService;
	}




	[HttpPost("submit")]
	public async Task<IActionResult> SubmitFile(StudentFormSubmitDTO studentFormSubmitDTO)
	{
		var customResponse = new CustomResponse<dynamic>();

		// Save submitted rar file
		bool saveFileSuccess = await StudentFileSubmitService.SaveStudentFileAsync(studentFormSubmitDTO.File);

		if (!saveFileSuccess)
		{
			customResponse.StatusCode = StatusCodes.Status500InternalServerError;
			customResponse.Message = "Failed to submit file. Only .rar files are allowed.";
			return HttpsUtility.ReturnActionResult(customResponse);
		}

		// Extract the submitted file
		StudentFileSubmitService.ExtractSavedFile();

		// Start grading
		var createStudentSubmissionDetailsDTO = await studentService.GradeStudentMark(studentFormSubmitDTO.SemesterId);
		createStudentSubmissionDetailsDTO.StudentId = studentFormSubmitDTO.StudentId;
		createStudentSubmissionDetailsDTO.SemesterId = studentFormSubmitDTO.SemesterId;


		try
		{
			await StudentFileSubmitService.CleanUpAllFiles();
		}
		catch (Exception)
		{
			customResponse.StatusCode = StatusCodes.Status500InternalServerError;
			customResponse.Message = "An error occurred while deleting student's files.";
			return HttpsUtility.ReturnActionResult(customResponse);
		}

		// Create (insert) new grading into DB
		bool createSuccessful = await studentService.CreateStudentSubmissionDetailsBySemester(createStudentSubmissionDetailsDTO);


		// Grade failure
		if (!createSuccessful)
		{
			customResponse.StatusCode = StatusCodes.Status500InternalServerError;
			customResponse.Message = "Failed to grade mark. Please submit again.";
		}
		else
		{
			customResponse.StatusCode = StatusCodes.Status201Created;
			customResponse.Message = "Grading successfully completed.";
		}
		return HttpsUtility.ReturnActionResult(customResponse);
	}




	[HttpGet("last-submissions")]
	public async Task<IActionResult> GetListStudentLastSubmissionAsync([FromQuery] int semesterId)
	{
		List<GetStudentSubmissionDetailsDTO> list = await studentService.GetListStudentLastSubmissionBySemester(semesterId);

		if (list == null || list.Count <= 0)
			return NotFound();

		return Ok(list);

	}



	[HttpGet("submission-detail")]
	public async Task<IActionResult> GetStudentSubmissionDetailsAsync([FromQuery] int submissionId)
	{
		CustomResponse<GetStudentSubmissionDetailsDTO> customResponse = await studentService.GetStudentSubmissionDetails(submissionId);
		return HttpsUtility.ReturnActionResult(customResponse);
	}

}



public sealed class StudentFormSubmitDTO
{
	[Required]
	public int SemesterId { get; set; }

	[Required]
	public Guid StudentId { get; set; }

	[Required]
	public IFormFile File { get; set; } = null!;
}
