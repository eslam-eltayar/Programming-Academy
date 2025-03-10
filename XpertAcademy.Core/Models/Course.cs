using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.Enums;

namespace XpertAcademy.Core.Models
{
    public class Course : BaseModel
    {
        public string TitleAR { get; set; } = default!;
        public string TitleEN { get; set; } = default!;

        public string ExternalDescriptionAR { get; set; } = default!;
        public string ExternalDescriptionEN { get; set; } = default!;
        
        public string InternalDescriptionAR { get; set; } = default!;
        public string InternalDescriptionEN { get; set; } = default!;

        public string ImageUrl { get; set; } = default!;
        public decimal Price { get; set; } = 0m;

        public CourseStatus CourseStatus { get; set; }
        public int Duration { get; set; }

        public decimal Discount { get; set; } = 0m;
        public decimal PriceAfterDiscount { get; set; }

        public ICollection<CourseContent> CourseContents { get; set; } = new HashSet<CourseContent>();
        public ICollection<Stud_Projects> Stud_Projects { get; set; } = new HashSet<Stud_Projects>();


        public ICollection<Stud_Review> Stud_Reviews { get; set; } = new HashSet<Stud_Review>();
    }
}
