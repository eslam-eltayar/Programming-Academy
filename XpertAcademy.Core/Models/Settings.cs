using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpertAcademy.Core.Models
{
    public class Settings : BaseModel
    {
        public int TraineesCount { get; set; }
        public int TrainersCount { get; set; }
        public int ProjectsCount { get; set; }
        public int CoursesCount { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AddressAR { get; set; }
        public string AddressEN { get; set; }

        public string FacebookAccount { get; set; }
        public string InstagramAccount { get; set; }
        public string LinkedInAccount { get; set; }
        public string TiktokAccount { get; set; }
    }
}
