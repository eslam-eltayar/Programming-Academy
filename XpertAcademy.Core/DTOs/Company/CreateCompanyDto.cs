using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpertAcademy.Core.DTOs.Company
{
    public class CreateCompanyDto
    {
        public string nameAR { get; set; }
        public string nameEN { get; set; }

        public IFormFile logo { get; set; }
    }
}
