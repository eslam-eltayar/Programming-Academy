using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XpertAcademy.Core.DTOs.Stud_Projects;
using XpertAcademy.Core.Services;

namespace XpertAcademy.APIs.Controllers
{
    public class Stud_ProjectsController : ApiBaseController
    {
        private readonly IStud_ProjectService _projectService;

        public Stud_ProjectsController(IStud_ProjectService ProjectService)
        {
            _projectService = ProjectService;
        }

        [HttpPost("AddNewStud_Project")]
        public async Task<ActionResult<Stud_ProjectToReturnDto>> AddNewProject([FromBody] AddNewProjectDto dto)
        {
            try
            {
                var project = await _projectService.AddNewProject(dto);

                return Ok(project);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"{ex.Message}" });
            }
        }

        [HttpGet("GetAllStud_Projects/{courseId}")]
        public async Task<ActionResult<IReadOnlyList<Stud_ProjectToReturnDto>>> GetAllStud_Projects(int courseId)
        {
            try
            {
                var projects = await _projectService.GetAllStud_ProjectsAsync(courseId);

                return Ok(projects);

            }
            catch (Exception ex)
            {

                return NotFound(new { Message = $"{ex.Message}" });
            }
        }
    }
}
