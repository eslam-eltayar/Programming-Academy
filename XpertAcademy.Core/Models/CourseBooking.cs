using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpertAcademy.Core.Models
{
    public class CourseBooking : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CourseAddress { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        
    }
}
