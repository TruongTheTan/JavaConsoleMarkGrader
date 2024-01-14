using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class StudentSemesterSubmission
    {
        public StudentSemesterSubmission()
        {
            StudentSubmissionDetails = new HashSet<StudentSubmissionDetail>();
        }

        public string StudentId { get; set; } = null!;
        public int SemesterId { get; set; }
        public int? TotalSubmission { get; set; }

        public virtual AspNetUser Student { get; set; } = null!;
        public virtual ICollection<StudentSubmissionDetail> StudentSubmissionDetails { get; set; }
    }
}
