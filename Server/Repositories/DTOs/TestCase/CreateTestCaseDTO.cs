using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace Repositories.DTO.TestCase
{
	public class CreateTestCaseDTO
	{
		[Required, MinLength(1)]
		[NotNull, ListNotEmptyString(ErrorMessage = "Inputs must not contain empty string ")]
		public List<string>? Input { get; set; }



		[Required, MinLength(1)]
		[NotNull, ListNotEmptyString(ErrorMessage = "Ouputs must not contain empty string ")]
		public List<string>? Output { get; set; }



		[Required, Range(1, 10), NotNull]
		public int? Mark { get; set; }



		[Required]
		public bool IsInputArray { get; set; } = false;


		[Required]
		public bool IsInputByLine { get; set; } = false;


		[Required, Range(0, int.MaxValue), NotNull]
		public int? SemesterId { get; set; }
	}
}
