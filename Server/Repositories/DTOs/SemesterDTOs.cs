using System.ComponentModel.DataAnnotations;

namespace Repositories.DTOs
{
	public class GetSemesterDTO
	{
		public int Id { get; set; }
		public string? SemesterName { get; set; }
	}


	public class CreateSemesterDTO
	{
		[Required]
		public string? SemesterName { get; set; }
	}


	public class UpdateSemesterDTO
	{
		[Required]
		public int Id { get; set; }

		[Required]
		public string? SemesterName { get; set; }
	}
}
