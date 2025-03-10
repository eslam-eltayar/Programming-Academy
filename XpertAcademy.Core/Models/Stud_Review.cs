using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.Enums;

namespace XpertAcademy.Core.Models
{
    public class Stud_Review : BaseModel
    {
        public string Stud_NameAR { get; set; } = default!;
        public string Stud_NameEN { get; set; } = default!;

        public string? Stud_SM_Link { get; set; } = default!; // social media link

        public string Stud_ImageUrl { get; set; } = default!;
        public string ReviewAR { get; set; } = default!;
        public string ReviewEN { get; set; } = default!;

        public ReviewType ReviewType { get; set; }

        public int? CourseId { get; set; }
        public Course? Course { get; set; } = default!;

    }
}
