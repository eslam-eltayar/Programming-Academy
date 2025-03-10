using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.DTOs.Booking;
using XpertAcademy.Core.DTOs.Pagination;

namespace XpertAcademy.Core.Services
{
    public interface IBookingService
    {
        Task<BookingToReturnDto> CreateNewBooking(CreateBookingDto dto);
        Task<IReadOnlyList<BookingToReturnDto>> GetAllBookingsAsync(PaginationDto dto);
        Task<int> GetAllBookingCount();

        Task<bool> DeleteCourseBookingAsync(int bookingId);
        Task<bool> DeleteWorkshopBookingAsync(int bookingId);

        Task<WorkshopBookingDto> CreateWorkshopBookingAsync(CreateWorkshopBookingDto dto);
        Task<IReadOnlyList<WorkshopBookingDto>> GetAllWorkshopBookingsAsync(PaginationDto dto);
        Task<int> GetAllWorkshopBookingCount();
    }
}
