using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XpertAcademy.APIs.Helpers;
using XpertAcademy.Core.DTOs.Course;
using XpertAcademy.Core.DTOs.Pagination;
using XpertAcademy.Core.Services;

namespace XpertAcademy.APIs.Controllers
{
    public class CoursesController : ApiBaseController
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost("AddNewCourse")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<CourseToReturnDto>> AddNewCourse([FromForm] CreateCourseDto dto)
        {
            try
            {
                var course = await _courseService.AddNewCourse(dto, dto.Contents);

                return Ok(course);
            }
            catch (Exception ex)
            {

                return BadRequest(new { Message = $"{ex.Message}" });
            }
        }

        [HttpGet("GetCourseDetails/{id}")]
        public async Task<ActionResult<CourseToReturnDto>> GetCourseDetails(int id)
        {
            try
            {
                var course = await _courseService.GetCourseDetailsAsync(id);

                return Ok(course);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = $"{ex.Message}" });
            }
        }

        [HttpGet("GetAllCourses")]
        public async Task<ActionResult<Pagination<IReadOnlyList<AllCoursesToReturnDto>>>> GetAllCourses([FromQuery] PaginationDto paginationDto)
        {
            try
            {
                var courses = await _courseService.GetAllCoursesAsync(paginationDto);

                int count = await _courseService.GetAllCoursesCount();

                return Ok(new Pagination<AllCoursesToReturnDto>(paginationDto.PageIndex, paginationDto.PageSize, courses, count));
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = $"{ex.Message}" });
            }
        }

        [HttpPut("EditCourse/{courseId}")]
        public async Task<ActionResult<CourseToReturnDto>> EditCourse(int courseId, [FromForm] CreateCourseDto dto)
        {
            try
            {
                var course = await _courseService.UpdateCourseAsync(courseId, dto);

                return Ok(course);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"{ex.Message}" });
            }
        }

        [HttpDelete("DeleteCourse/{courseId}")]
        public async Task<ActionResult> DeleteCourse(int courseId)
        {
            try
            {
                var result = await _courseService.DeleteCourseAsync(courseId);

                return Ok(new { message = "Course deleted successfully." });

            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"{ex.Message}" });
            }
        }

    }
}
