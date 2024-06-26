﻿using System;
using System.Collections.Generic;

namespace Server_4.DAL.Models
{
    public partial class TestCase
    {
        public int Id { get; set; }
        public string? Input { get; set; }
        public string? Output { get; set; }
        public byte? Mark { get; set; }
        public bool? IsInputArray { get; set; }
        public bool? IsInputByLine { get; set; }
        public int? SemesterId { get; set; }

        public virtual Semester? Semester { get; set; }
    }
}
