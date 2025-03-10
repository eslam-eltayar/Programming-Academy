using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpertAcademy.Core.DTOs.Review
{
    public class CreateReviewDto
    {
        public string studentNameAR { get; set; }
        public string studentNameEN { get; set; }

        public string? studentSMLink { get; set; }

        public string reviewType { get; set; }

        public string reviewAR { get; set; }
        public string reviewEN { get; set; }

        public int? courseId { get; set; }

        public IFormFile image { get; set; }
    }
}
