using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.Enums;
using XpertAcademy.Core.Models;

namespace XpertAcademy.Core.DTOs.Workshop
{
    public class CreateWorkshopDto
    {
        public string TitleAR { get; set; }
        public string TitleEN { get; set; }

        public string DescriptionAR { get; set; }
        public string DescriptionEN { get; set; }

        public DateTime Date { get; set; }
        public IFormFile Image { get; set; }

        public string Type { get; set; }

        public decimal? Price { get; set; } 

        public List<WorkshopAudienceDto> WorkshopAudiences { get; set; } = new List<WorkshopAudienceDto>();

    }
}
