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

        public Guid StudentId { get; set; }
        public int SemesterId { get; set; }
        public int? TotalSubmission { get; set; }

        public virtual Semester Semester { get; set; } = null!;
        public virtual User Student { get; set; } = null!;
        public virtual ICollection<StudentSubmissionDetail> StudentSubmissionDetails { get; set; }
    }
}
