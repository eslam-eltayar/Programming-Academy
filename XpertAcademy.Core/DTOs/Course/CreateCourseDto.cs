using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpertAcademy.Core.DTOs.Course
{
    public class CreateCourseDto
    {
        public string TitleAR { get; set; } = default!;
        public string TitleEN { get; set; } = default!;

        public string ExternalDescriptionAR { get; set; } = default!;
        public string ExternalDescriptionEN { get; set; } = default!;

        public string InternalDescriptionAR { get; set; } = default!;
        public string InternalDescriptionEN { get; set; } = default!;

        public decimal Price { get; set; } = 0m;
        public int Duration { get; set; }
        public string CourseStatus { get; set; }

        public IFormFile Image { get; set; }

        //[FromForm]
        public List<AddContentDto> Contents { get; set; } = new List<AddContentDto>();
        public List<AddVideotDto> Videos { get; set; } = new List<AddVideotDto>();

    }
}
