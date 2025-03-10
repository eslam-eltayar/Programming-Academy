using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.Models;

namespace XpertAcademy.Core.Specifications.Projects
{
    public class ProjectsOfCourseSpecification : BaseSpecification<Stud_Projects>
    {
        public ProjectsOfCourseSpecification(int courseId) :
            base(s => s.CourseId == courseId)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(s => s.Course);
        }
    }
}
