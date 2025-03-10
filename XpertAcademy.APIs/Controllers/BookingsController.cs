using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XpertAcademy.APIs.Helpers;
using XpertAcademy.Core.DTOs.Booking;
using XpertAcademy.Core.DTOs.Pagination;
using XpertAcademy.Core.Models;
using XpertAcademy.Core.Services;

namespace XpertAcademy.APIs.Controllers
{
    public class BookingsController : ApiBaseController
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("CreateNewBooking")]
        public async Task<ActionResult<BookingToReturnDto>> CreateNewBooking([FromBody] CreateBookingDto dto)
        {
            try
            {
                var booking = await _bookingService.CreateNewBooking(dto);
                return Ok(booking);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"{ex.Message}" });
            }
        }

        [HttpGet("GetAllCoursesBookings")]
        public async Task<ActionResult<Pagination<IReadOnlyList<BookingToReturnDto>>>> GetAllCoursesBookings([FromQuery] PaginationDto dto)
        {
            try
            {
                var bookings = await _bookingService.GetAllBookingsAsync(dto);

                int count = await _bookingService.GetAllBookingCount();

                return Ok(new Pagination<BookingToReturnDto>(dto.PageIndex, dto.PageSize, bookings, count));
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = $"{ex.Message}" });
            }
        }

        [HttpDelete("DeleteCourseBooking/{bookingId}")]
        public async Task<ActionResult> DeleteCourseBooking(int bookingId)
        {
            try
            {
                var result = await _bookingService.DeleteCourseBookingAsync(bookingId);

                return Ok(new { Message = "Course Booking Deleted Successfuly yastaaaaaaa" });
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = $"{ex.Message}" });
            }
        }


        [HttpPost("CreateWorkshopBooking")]
        public async Task<ActionResult<WorkshopBookingDto>> CreateWorkshopBooking([FromBody] CreateWorkshopBookingDto dto)
        {
            try
            {
                var booking = await _bookingService.CreateWorkshopBookingAsync(dto);

                return Ok(booking);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"{ex.Message}" });
            }
        }

        [HttpGet("GetAllWorkshopBookings")]
        public async Task<ActionResult<IReadOnlyList<WorkshopBookingDto>>> GetAllWorkshopBookings([FromQuery] PaginationDto dto)
        {
            try
            {
                var bookings = await _bookingService.GetAllWorkshopBookingsAsync(dto);

                int count = await _bookingService.GetAllWorkshopBookingCount();

                return Ok(new Pagination<WorkshopBookingDto>(dto.PageIndex, dto.PageSize, bookings, count));
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = $"{ex.Message}" });
            }
        }

        [HttpDelete("DeleteWorkshopBooking/{bookingId}")]
        public async Task<ActionResult> DeleteWorkshopBooking(int bookingId)
        {
            try
            {
                var result = await _bookingService.DeleteWorkshopBookingAsync(bookingId);

                return Ok(new { Message = "Course Booking Deleted Successfuly yastaaaaaaa" });
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = $"{ex.Message}" });
            }
        }

    }

}

