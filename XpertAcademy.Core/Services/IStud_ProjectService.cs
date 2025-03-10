using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.DTOs.Stud_Projects;

namespace XpertAcademy.Core.Services
{
    public interface IStud_ProjectService
    {
        Task<Stud_ProjectToReturnDto> AddNewProject(AddNewProjectDto dto);
        Task<IReadOnlyList<Stud_ProjectToReturnDto>> GetAllStud_ProjectsAsync(int courseId);
    }
}
