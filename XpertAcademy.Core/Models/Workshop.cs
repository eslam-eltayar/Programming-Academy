using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.Enums;

namespace XpertAcademy.Core.Models
{
    public class Workshop : BaseModel
    {
        public string TitleAR { get; set; }
        public string TitleEN { get; set; }

        public string DescriptionAR { get; set; }
        public string DescriptionEN { get; set; }

        public DateTime Date { get; set; }
        public string Image { get; set; }

        public decimal Price { get; set; } = 0m;
        public WorkshopType Type { get; set; }

        public ICollection<WorkshopAudience> WorkshopAudiences { get; set; } = new HashSet<WorkshopAudience>();
    }
}
