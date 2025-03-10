using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpertAcademy.Core.DTOs.Course
{
    public class AllCoursesToReturnDto
    {
        public int CourseId { get; set; }

        public string TitleAR { get; set; }
        public string TitleEN { get; set; }

        public int Duration { get; set; }
        
        public string ExternalDescriptionAR { get; set; } = default!;
        public string ExternalDescriptionEN { get; set; } = default!;

        public decimal Price { get; set; }
        public string CourseStatus { get; set; }

        public string ImageUrl { get; set; }
        public decimal PriceAfterDiscount { get; set; }

    }
}
