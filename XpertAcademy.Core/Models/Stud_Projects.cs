using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpertAcademy.Core.Models
{
    public class Stud_Projects : BaseModel
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public string Project_Link { get; set; }
    }
}
