using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpertAcademy.Core.DTOs.Review
{
    public class ReviewToReturnDto
    {
        public int ReviewId { get; set; }

        public string StudentNameAR { get; set; }
        public string StudentNameEN { get; set; }

        public string? StudentSMLink { get; set; }

        public string ReviewType { get; set; }

        public string ReviewAR { get; set; }
        public string ReviewEN { get; set; }

        public int? CourseId { get; set; }
        public string Image { get; set; }
    }
}
