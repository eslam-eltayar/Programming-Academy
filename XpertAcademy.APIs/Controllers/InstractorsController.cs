using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XpertAcademy.APIs.Helpers;
using XpertAcademy.Core.DTOs.Instractor;
using XpertAcademy.Core.DTOs.Pagination;
using XpertAcademy.Core.Services;
using XpertAcademy.Service.Services;

namespace XpertAcademy.APIs.Controllers
{

    public class InstractorsController : ApiBaseController
    {
        private readonly IInstractorService _instractorService;

        public InstractorsController(IInstractorService instractorService)
        {
            _instractorService = instractorService;
        }

        [HttpGet("GetAllInstractors")]
        public async Task<ActionResult<Pagination<IReadOnlyList<InstractorToReturnDto>>>> GetAllInstractors([FromQuery] PaginationDto dto)
        {
            try
            {
                var instractors = await _instractorService.GetAllInstractorsAsync(dto);

                int count = await _instractorService.GetInstractorsCount();

                return Ok(new Pagination<InstractorToReturnDto>(dto.PageIndex, dto.PageSize, instractors, count));
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = $"{ex.Message}" });
            }
        }

        [HttpPost("AddNewInstractor")]
        public async Task<ActionResult<InstractorToReturnDto>> AddNewInstractor([FromForm] CreateInstractorDto dto)
        {
            try
            {
                var course = await _instractorService.AddNewInstractor(dto);

                return Ok(course);
            }
            catch (Exception ex)
            {

                return BadRequest(new { Message = $"{ex.Message}" });
            }
        }

        [HttpPut("EditInstractor/{instractorId}")]
        public async Task<ActionResult<InstractorToReturnDto>> EditInstractor(int instractorId, [FromForm] CreateInstractorDto dto)
        {
            try
            {
                var instractor = await _instractorService.UpdateInstractorAsync(instractorId, dto);

                return Ok(instractor);
            }
            catch (Exception ex)
            {

                return BadRequest(new { Message = $"{ex.Message}" });
            }
        }

        [HttpDelete("DeleteInstractor/{instractorId}")]
        public async Task<ActionResult> DeleteInstractor(int instractorId)
        {
            try
            {
                var result = await _instractorService.DeleteInstractorAsync(instractorId);

                return Ok(new { message = "Instractor deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"{ex.Message}" });
            }
        }

    }
}
