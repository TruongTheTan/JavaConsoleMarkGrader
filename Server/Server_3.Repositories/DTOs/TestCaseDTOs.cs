using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Repositories.DTOs;

public class CreateTestCaseDTO
{
	[Required, MinLength(1)]
	[NotNull, ListNotContainEmptyString(ErrorMessage = "Inputs must not contain empty string ")]
	public List<string>? Inputs { get; set; }



	[Required, MinLength(1)]
	[NotNull, ListNotContainEmptyString(ErrorMessage = "Outputs must not contain empty string ")]
	public List<string>? Outputs { get; set; }



	[Required, NotNull, Range(1, 10, ErrorMessage = "Mark must between 1 to 10")]
	public byte? Mark { get; set; }


	[Required]
	public bool? IsInputByLine { get; set; }


	[Required, Range(1, int.MaxValue), NotNull]
	public int? SemesterId { get; set; }
}







public class GetTestCaseDTO
{
	public int Id { get; set; }
	public List<string>? Inputs { get; set; }
	public List<string>? Outputs { get; set; }
	public byte? Mark { get; set; }
	public bool IsInputByLine { get; set; } = false;
	public int? SemesterId { get; set; }
	public string? SemesterName { get; set; }
}





public class UpdateTestCaseDTO
{

	[Required]
	public int Id { get; set; }


	[Required, MinLength(1)]
	[NotNull, ListNotContainEmptyString(ErrorMessage = "Inputs must not contain empty string ")]
	public List<string>? Input { get; set; }


	[Required, MinLength(1)]
	[NotNull, ListNotContainEmptyString(ErrorMessage = "Outputs must not contain empty string ")]
	public List<string>? Output { get; set; }


	[Required, NotNull, Range(1, 10, ErrorMessage = "Mark must between 1 to 10")]
	public byte? Mark { get; set; }


	[Required]
	public bool IsInputByLine { get; set; }


	[Required, Range(1, int.MaxValue), NotNull]
	public int? SemesterId { get; set; }
}

