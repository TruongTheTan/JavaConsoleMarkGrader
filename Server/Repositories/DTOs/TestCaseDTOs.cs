using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Repositories.DTOs
{
	public class CreateTestCaseDTO
	{
		[Required, MinLength(1)]
		[NotNull, ListNotContainEmptyString(ErrorMessage = "Inputs must not contain empty string ")]
		public List<string>? Input { get; set; }



		[Required, MinLength(1)]
		[NotNull, ListNotContainEmptyString(ErrorMessage = "Ouputs must not contain empty string ")]
		public List<string>? Output { get; set; }



		[Required, Range(1, 10), NotNull]
		public int? Mark { get; set; }


		[Required]
		public bool? IsInputArray { get; set; }


		[Required]
		public bool? IsInputByLine { get; set; }


		[Required, Range(1, int.MaxValue), NotNull]
		public int? SemesterId { get; set; }


		[JsonIgnore]
		public bool CheckValidInputType
		{
			get
			{
				if (IsInputArray != IsInputByLine || IsInputByLine != IsInputArray)
					return true;

				return false;
			}
		}

	}










	public class GetTestCaseDTO
	{
		public int Id { get; set; }
		public List<string>? Input { get; set; }
		public List<string>? Output { get; set; }
		public byte? Mark { get; set; }
		public bool IsInputArray { get; set; } = false;
		public bool IsInputByLine { get; set; } = false;
		public int? SemesterId { get; set; }
	}





	public class UpdateTestCaseDTO : GetTestCaseDTO
	{
	}
}
