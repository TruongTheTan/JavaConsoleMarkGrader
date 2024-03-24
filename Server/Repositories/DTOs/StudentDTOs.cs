using System.ComponentModel.DataAnnotations;

namespace Repositories.DTOs
{
	public class GetStudentDTO
	{
		public string? Id { get; set; }
		public short? TotalMark { get; set; }
		public string? QuestionDescription { get; set; }
		public string? StudentNote { get; set; }
		public string? GradingTime { get; set; }
	}


	public class GetStudentSubmissionDetailsDTO
	{
		public int Id { get; set; }
		public string? StudentId { get; set; }
		public int SemesterId { get; set; }
		public short? TotalMark { get; set; }
		public string? QuestionDescription { get; set; }
		public string? StudentNote { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? GradingTime { get; set; }
	}





	public class CreateStudentSubmissionDetailsDTO
	{
		public string? StudentId { get; set; }
		public int SemesterId { get; set; }
		public short? TotalMark { get; set; }
		public string? QuestionDescription { get; set; }
		public string? StudentNote { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? GradingTime { get; set; }
	}
}
