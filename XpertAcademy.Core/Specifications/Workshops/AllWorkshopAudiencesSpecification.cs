using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.Models;

namespace XpertAcademy.Core.Specifications.Workshops
{
    public class AllWorkshopAudiencesSpecification : BaseSpecification<WorkshopAudience>
    {
        public AllWorkshopAudiencesSpecification(int workshopId)
            : base(w => w.WorkshopId == workshopId)
        {

        }
    }
}
