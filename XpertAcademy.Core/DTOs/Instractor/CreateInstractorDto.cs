using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpertAcademy.Core.DTOs.Instractor
{
    public class CreateInstractorDto
    {
        public string nameAR { get; set; }
        public string nameEN { get; set; }

        public string aboutAR { get; set; }
        public string aboutEN { get; set; }
            
        public IFormFile image { get; set; }

    }
}
