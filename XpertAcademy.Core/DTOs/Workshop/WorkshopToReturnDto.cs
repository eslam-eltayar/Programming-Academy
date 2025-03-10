using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpertAcademy.Core.DTOs.Workshop
{
    public class WorkshopToReturnDto
    {
        public int WorkshopId { get; set; }

        public string TitleAR { get; set; }
        public string TitleEN { get; set; }

        public string DescriptionAR { get; set; }
        public string DescriptionEN { get; set; }

        public string Date { get; set; }
        public string DateEN { get; set; }

        public string Image { get; set; }

        public string Type { get; set; }

        public decimal Price { get; set; }

        public IReadOnlyList<WorkshopAudienceDto> WorkshopAudiences { get; set; }

    }
}
