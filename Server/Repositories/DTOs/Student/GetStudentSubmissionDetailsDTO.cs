namespace Repositories.DTOs.Student
{
    public class GetStudentSubmissionDetailsDTO
    {
        public int Id { get; set; }
        public Guid? StudentId { get; set; }
        public int SemesterId { get; set; }
        public short? TotalMark { get; set; }
        public string? QuestionDescription { get; set; }
        public string? StudentNote { get; set; }
        public DateTime? GradingTime { get; set; }
    }
}
