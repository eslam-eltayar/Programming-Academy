using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpertAcademy.Core.DTOs.Instractor
{
    public class InstractorToReturnDto
    {
        public int InstractorId { get; set; }

        public string NameAR { get; set; }
        public string NameEN { get; set; }

        public string AboutAR { get; set; }
        public string AboutEN { get; set; }

        public string Image { get; set; }
    }
}
