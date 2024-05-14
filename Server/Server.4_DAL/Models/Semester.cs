using System;
using System.Collections.Generic;

namespace Server_4.DAL.Models
{
    public partial class Semester
    {
        public Semester()
        {
            TestCases = new HashSet<TestCase>();
        }

        public int Id { get; set; }
        public string? SemesterName { get; set; }

        public virtual ICollection<TestCase> TestCases { get; set; }
    }
}
