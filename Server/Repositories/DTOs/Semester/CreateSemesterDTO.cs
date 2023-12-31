using System.ComponentModel.DataAnnotations;

namespace Repositories.DTOs.Semester
{
    public class CreateSemesterDTO
    {
        [Required]
        public string? SemesterName { get; set; }
    }
}
