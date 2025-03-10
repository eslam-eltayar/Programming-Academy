using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.DTOs.Pagination;
using XpertAcademy.Core.Models;

namespace XpertAcademy.Core.Specifications.Booking
{
    public class AllWorkshopBookingSpecification : BaseSpecification<WorkshopBooking>
    {
        public AllWorkshopBookingSpecification(PaginationDto paginationDto)

        {
            AddIncludes();

            ApplyOrderByDescending(b => b.Id);

            var pageIndexHelper = 0;

            if ((paginationDto.PageIndex - 1) < 0)
            {
                pageIndexHelper = 0;
            }
            else
            {
                pageIndexHelper = paginationDto.PageIndex - 1;
            }

            ApplyPagination(pageIndexHelper * paginationDto.PageSize, paginationDto.PageSize);
        }

        public AllWorkshopBookingSpecification()
        {
            
        }

        private void AddIncludes()
        {
            Includes.Add(w => w.Workshop);
        }
    }
}
