using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpertAcademy.Core.Models
{
    public class Instractor : BaseModel
    {
        public string NameAR { get; set; } = default!;
        public string NameEN { get; set; } = default!;

        public string AboutAR { get; set; } = default!;
        public string AboutEN { get; set; } = default!;

        public string ImageUrl { get; set; } = default!;

    }
}
