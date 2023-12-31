using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class StudentSubmissionDetail
    {
        public int Id { get; set; }
        public Guid? StudentId { get; set; }
        public int SemesterId { get; set; }
        public short? TotalMark { get; set; }
        public string? QuestionDescription { get; set; }
        public string? StudentNote { get; set; }
        public DateTime? GradingTime { get; set; }

        public virtual StudentSemesterSubmission? S { get; set; }
    }
}
