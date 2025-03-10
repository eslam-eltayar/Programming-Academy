using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpertAcademy.Core.Models
{
    public class WorkshopAudience : BaseModel
    {
        public string TitleAR { get; set; }
        public string TitleEN { get; set; }

        public string DescriptionAR { get; set; }
        public string DescriptionEN { get; set; }

        public int WorkshopId { get; set; }
        public Workshop Workshop { get; set; }

    }
}
