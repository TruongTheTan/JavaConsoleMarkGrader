using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Repositories.DTOs;

public class GetSemesterDTO
{
	public int Id { get; set; }
	public string? SemesterName { get; set; }
}



public class CreateSemesterDTO
{
	[Required(ErrorMessage = "Semester name must not empty"), NotNull]
	public string? SemesterName { get; set; }
}



public class UpdateSemesterDTO
{
	[Required(ErrorMessage = "ID must not empty")]
	public int Id { get; set; }

	[Required(ErrorMessage = "Semester name must not empty")]
	public string? SemesterName { get; set; }
}

