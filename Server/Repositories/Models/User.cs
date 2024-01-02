﻿using System;
using System.Collections.Generic;

namespace Repositories.Models
{
    public partial class User
    {
        public User()
        {
            StudentSemesterSubmissions = new HashSet<StudentSemesterSubmission>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<StudentSemesterSubmission> StudentSemesterSubmissions { get; set; }
    }
}