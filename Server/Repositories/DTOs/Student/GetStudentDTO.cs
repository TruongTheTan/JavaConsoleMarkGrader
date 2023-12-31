namespace Repositories.DTOs.Student
{
    public class GetStudentDTO
    {
        public Guid Id { get; set; }
        public short? TotalMark { get; set; }
        public string? QuestionDescription { get; set; }
        public string? StudentNote { get; set; }
        public string? GradingTime { get; set; }
    }
}
