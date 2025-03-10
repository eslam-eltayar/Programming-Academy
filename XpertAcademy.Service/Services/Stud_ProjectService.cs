using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.DTOs.Stud_Projects;
using XpertAcademy.Core.Models;
using XpertAcademy.Core.Repositories;
using XpertAcademy.Core.Services;
using XpertAcademy.Core.Specifications.Projects;

namespace XpertAcademy.Service.Services
{
    public class Stud_ProjectService : IStud_ProjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public Stud_ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Stud_ProjectToReturnDto> AddNewProject(AddNewProjectDto dto)
        {
            if (dto == null) { throw new Exception("Invalid input. The Body cannot be null"); }

            var project = new Stud_Projects
            {
                CourseId = dto.CourseId,
                Project_Link = dto.ProjectLink

            };

            _unitOfWork.Repository<Stud_Projects>().Add(project);

            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                throw new Exception("An Error When saving changes");

            return new Stud_ProjectToReturnDto
            {
                Id = project.Id,
                CourseId = project.CourseId,
                ProjectLink = project.Project_Link
            };

        }

        public async Task<IReadOnlyList<Stud_ProjectToReturnDto>> GetAllStud_ProjectsAsync(int courseId)
        {
            var spec = new ProjectsOfCourseSpecification(courseId);

            var projects = await _unitOfWork.Repository<Stud_Projects>().GetAllWithSpecAsync(spec);

            if (projects == null || !projects.Any())
                throw new Exception("There's No Links Founded!");

            return projects.Select(p => new Stud_ProjectToReturnDto
            {
                Id = p.Id,
                CourseId = p.CourseId,
                ProjectLink = p.Project_Link,
                CourseName = p.Course.TitleAR

            }).ToList().AsReadOnly();
        }
    }
}
