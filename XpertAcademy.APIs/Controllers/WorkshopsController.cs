using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XpertAcademy.APIs.Helpers;
using XpertAcademy.Core.DTOs.Pagination;
using XpertAcademy.Core.DTOs.Workshop;
using XpertAcademy.Core.Models;
using XpertAcademy.Core.Services;

namespace XpertAcademy.APIs.Controllers
{
    public class WorkshopsController : ApiBaseController
    {
        private readonly IWorkshopService _workshopService;

        public WorkshopsController(IWorkshopService workshopService)
        {
            _workshopService = workshopService;
        }

        [HttpPost("AddNewWorkshop")]
        public async Task<ActionResult<WorkshopToReturnDto>> AddNewWorkshop([FromForm] CreateWorkshopDto dto)
        {
            try
            {
                var workshop = await _workshopService.AddNewWorkshopAsync(dto);

                return Ok(workshop);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"{ex.Message}" });
            }
        }

        [HttpPut("EditWorkshop/{workshopId}")]
        public async Task<ActionResult<WorkshopToReturnDto>> EditWorkshop(int workshopId, [FromForm] CreateWorkshopDto dto)
        {
            try
            {
                var workshop = await _workshopService.EditWorkshopAsync(workshopId, dto);

                return Ok(workshop);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = $"{ex.Message}" });
            }
        }

        [HttpGet("GetAllWorkshops")]
        public async Task<ActionResult<Pagination<IReadOnlyList<WorkshopToReturnDto>>>> GetAllMemberships([FromQuery] PaginationDto dto)
        {
            try
            {
                var workshops = await _workshopService.GetAllWorkshopsAsync(dto);

                int count = await _workshopService.GetAllWorkshopsCount();

                return Ok(new Pagination<WorkshopToReturnDto>(dto.PageIndex, dto.PageSize, workshops, count));
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = $"{ex.Message}" });
            }
        }


        [HttpGet("GetWorkshopDetails/{workshopId}")]
        public async Task<ActionResult<WorkshopToReturnDto>> GetWorkshopDetails(int workshopId)
        {
            try
            {
                var workshop = await _workshopService.GetWorkshopDetailsAsync(workshopId);

                return Ok(workshop);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = $"{ex.Message}" });
            }
        }

        [HttpDelete("DeleteWorkshop/{workshopId}")]
        public async Task<ActionResult> DeleteWorkshop(int workshopId)
        {
            try
            {
                var result = await _workshopService.DeleteWorkshopAsync(workshopId);

                return Ok(new { message = "Workshop deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"{ex.Message}" });
            }
        }
    }
}
