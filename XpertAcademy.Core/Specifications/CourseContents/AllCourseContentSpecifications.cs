using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.Models;

namespace XpertAcademy.Core.Specifications.CourseContents
{
    public class AllCourseContentSpecifications : BaseSpecification<CourseContent>
    {
        public AllCourseContentSpecifications(int courseId)
            :base(c=> c.CourseId == courseId)
        {
            
        }
    }
}
