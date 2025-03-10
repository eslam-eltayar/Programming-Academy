using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.DTOs.Pagination;
using XpertAcademy.Core.Models;

namespace XpertAcademy.Core.Specifications.Reviews
{
    public class StudentReviewsSpecifications : BaseSpecification<Stud_Review>
    {
        public StudentReviewsSpecifications()
            :base(r=>r.ReviewType == Enums.ReviewType.General)
        {
            ApplyOrderByDescending(r => r.Id);

            AddIncludes();

        }

        public StudentReviewsSpecifications(PaginationDto paginationDto)
        {
            ApplyOrderByDescending(r => r.Id);

            AddIncludes();

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

        public StudentReviewsSpecifications(int courseId)
            : base(s => s.CourseId == courseId)
        {
            ApplyOrderByDescending(r => r.Id);

            AddIncludes();
        }

       

        private void AddIncludes()
        {
            Includes.Add(s => s.Course);
        }
    }
}
