using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpertAcademy.Core.DTOs.Company
{
    public class CompanyToReturnDto
    {
        public int CompanyId { get; set; }

        public string Logo { get; set; }
        public string NameAR { get; set; }
        public string NameEN { get; set; }
    }
}
