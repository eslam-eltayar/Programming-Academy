﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpertAcademy.Core.DTOs.Stud_Projects
{
    public class Stud_ProjectToReturnDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string ProjectLink { get; set; }
        public string CourseName { get; set; }
    }
}
