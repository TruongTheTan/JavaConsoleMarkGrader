using System;
using System.Collections.Generic;

namespace Server_4.DAL.Models
{
    public partial class StudentSubmissionDetail
    {
        public int Id { get; set; }
        public string? StudentId { get; set; }
        public int? SemesterId { get; set; }
        public byte? TotalMark { get; set; }
        public string? QuestionDescription { get; set; }
        public string? StudentNote { get; set; }
        public DateTime? GradingTime { get; set; }

        public virtual StudentSemesterSubmission? S { get; set; }
    }
}
