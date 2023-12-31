using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class Semester
    {
        public Semester()
        {
            StudentSemesterSubmissions = new HashSet<StudentSemesterSubmission>();
            TestCases = new HashSet<TestCase>();
        }

        public int Id { get; set; }
        public string? SemesterName { get; set; }

        public virtual ICollection<StudentSemesterSubmission> StudentSemesterSubmissions { get; set; }
        public virtual ICollection<TestCase> TestCases { get; set; }
    }
}
