﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpertAcademy.Core.Models
{
    public class WorkshopBooking : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string? LinkedIn { get; set; }

        public int WorkshopId { get; set; }
        public Workshop Workshop { get; set; }
    }
}
