using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XpertAcademy.APIs.Helpers;
using XpertAcademy.Core.DTOs.Instractor;
using XpertAcademy.Core.DTOs.Pagination;
using XpertAcademy.Core.DTOs.Review;
using XpertAcademy.Core.Models;
using XpertAcademy.Core.Services;
using XpertAcademy.Service.Services;

namespace XpertAcademy.APIs.Controllers
{
    public class StudentReviewsController : ApiBaseController
    {
        private readonly IStud_ReviewService _reviewService;

        public StudentReviewsController(IStud_ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("AddNewReview")]
        public async Task<ActionResult<ReviewToReturnDto>> AddNewReview([FromForm] CreateReviewDto dto)
        {
            try
            {
                var review = await _reviewService.AddNewReview(dto);

                return Ok(review);
            }
            catch (Exception ex)
            {

                return BadRequest(new { Message = $"{ex.Message}" });
            }
        }

        [HttpGet("GetGeneralReviews")]
        public async Task<ActionResult<IReadOnlyList<ReviewToReturnDto>>> GetAllReviews()
        {
            try
            {
                var reviews = await _reviewService.GetGeneralReviewsAsync();

                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = $"{ex.Message}" });
            }
        }

        [HttpGet("GetAllReviewsForCourse/{courseId}")]
        public async Task<ActionResult<IReadOnlyList<ReviewToReturnDto>>> GetAllReviewsForCourse(int courseId)
        {
            try
            {
                var reviews = await _reviewService.GetAllReviewsForCourseAsync(courseId);

                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = $"{ex.Message}" });
            }
        }

        [HttpGet("GetAllReviews")]
        public async Task<ActionResult<Pagination<IReadOnlyList<ReviewToReturnDto>>>> GetAllReviews([FromQuery] PaginationDto dto)
        {
            try
            {
                var reviews = await _reviewService.GetAllReviewsAsync(dto);

                int count = await _reviewService.GetAllReviewsCount();

                return Ok(new Pagination<ReviewToReturnDto>(dto.PageIndex , dto.PageSize , reviews , count));
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = $"{ex.Message}" });
            }
        }


        [HttpPut("EditReview/{reviewId}")]
        public async Task<ActionResult<ReviewToReturnDto>> EditReview(int reviewId, [FromForm] CreateReviewDto dto)
        {
            try
            {
                var review = await _reviewService.UpdateReviewAsync(reviewId, dto);

                return Ok(review);
            }
            catch (Exception ex)
            {

                return BadRequest(new { Message = $"{ex.Message}" });
            }
        }

        [HttpDelete("DeleteReview/{reviewId}")]
        public async Task<ActionResult> DeleteReveiew(int reviewId)
        {
            try
            {
                var result = await _reviewService.DeleteReviewAsync(reviewId);

                return Ok(new { message = "Review deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"{ex.Message}" });
            }
        }

    }
}
