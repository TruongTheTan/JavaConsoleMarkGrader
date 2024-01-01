using Microsoft.AspNetCore.Mvc;
using Repositories.DTOs.Student;
using Services;
using Services.StudentService;


namespace MarkGrader.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentController : ControllerBase
	{
		private readonly IStudentService studentService;


		public StudentController(IStudentService studentService)
		{
			this.studentService = studentService;
		}



		[HttpGet("{id:Guid}")]
		public async Task<IActionResult> GetStudentById(Guid id)
		{
			var a = await studentService.GetStudentByIdAsync(id)!;


			if (a == null)
				return NotFound("Student not found");

			return Ok(a);

		}




		[HttpPost("submit")]
		public async Task<IActionResult> SubmitFile(int semesterId, Guid studentId, IFormFile file)
		{
			/*
			Request.Headers.TryGetValue("StudentId", out StringValues headerStudentIdValue);
			Request.Headers.TryGetValue("SemesterId", out StringValues headerSemesterIdValue);
			*/

			//Guid studentId = Guid.Parse(headerStudentIdValue[0].ToString());
			//int semesterId = Convert.ToInt32(headerSemesterIdValue[0].ToString());

			// Save submitted rar file
			bool saveFileSuccess = await StudentFileSubmitManager.SaveStudentFileAsync(file);


			if (!saveFileSuccess)
				return Problem("Only rar extension is allowed");

			// Extract the summited file
			StudentFileSubmitManager.ExtractSavedFile();


			// Start grading
			var createStudentSubmissionDetailsDTO = await studentService.GradeStudentMark(semesterId);
			createStudentSubmissionDetailsDTO.StudentId = studentId;
			createStudentSubmissionDetailsDTO.SemesterId = semesterId;


			try
			{
				Task deleteCompressFile = StudentFileSubmitManager.DeleteStudentCompressedFileAsync();
				Task deleteExtractedFile = StudentFileSubmitManager.DeleteStudentExtractedFileAsync();
			}
			catch (Exception)
			{
				return Problem("Something wrong when delete student's file");
			}

			// Create (insert) new grading into DB 
			/*
			bool createSuccesfully = await studentService.CreateStudentSubmissionDetailsBySemester(createStudentSubmissionDetailsDTO);

			if (!createSuccesfully)
			return Problem();
			*/
			return Ok(createStudentSubmissionDetailsDTO);
		}




		[HttpGet("student-last-submissions")]
		public async Task<IActionResult> GetListStudentLastSubmissionAsync([FromQuery] int semesterId)
		{
			List<GetStudentSubmissionDetailsDTO> list = await this.studentService.GetListStudentLastSubmissionBySemester(semesterId);

			if (list == null || list.Count <= 0)
				return NotFound();

			return Ok(list);

		}



		[HttpGet("submission-detail")]
		public async Task<IActionResult> GetStudentSubmissionDetailsAsync([FromQuery] int submissionId)
		{
			GetStudentSubmissionDetailsDTO studentSubmissionDetailsDTO = await this.studentService.GetStudentSubmissionDetails(submissionId);

			return Ok(studentSubmissionDetailsDTO);
		}
	}
}
