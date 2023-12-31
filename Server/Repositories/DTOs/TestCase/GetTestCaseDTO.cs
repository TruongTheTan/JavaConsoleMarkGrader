namespace Repositories.DTOs.TestCase
{
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
}
