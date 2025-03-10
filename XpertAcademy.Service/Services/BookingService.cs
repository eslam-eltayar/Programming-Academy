using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.DTOs.Booking;
using XpertAcademy.Core.DTOs.Pagination;
using XpertAcademy.Core.Models;
using XpertAcademy.Core.Repositories;
using XpertAcademy.Core.Services;
using XpertAcademy.Core.Specifications.Booking;
using XpertAcademy.Core.Specifications.Courses;

namespace XpertAcademy.Service.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BookingToReturnDto> CreateNewBooking(CreateBookingDto dto)
        {
            if (dto == null)
                throw new Exception("Invalid Input. The Data couldn't be null");

            var booking = new CourseBooking
            {
                Name = dto.Name,
                CourseAddress = dto.Address,
                CourseId = dto.CourseId,
                Email = dto.Email,
                Phone = dto.Phone,

            };

            _unitOfWork.Repository<CourseBooking>().Add(booking);

            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                throw new Exception("There's an error while saving changes");

            return new BookingToReturnDto
            {
                BookingId = booking.Id,
                Address = booking.CourseAddress,
                CourseId = booking.CourseId,
                Email = booking.Email,
                Phone = booking.Phone,
                Name = booking.Name
            };

        }

        public async Task<WorkshopBookingDto> CreateWorkshopBookingAsync(CreateWorkshopBookingDto dto)
        {
            if (dto == null)
                throw new Exception("invalid input. the body cannot be Empty!!");

            var booking = new WorkshopBooking
            {
                WorkshopId = dto.WorkshopId,
                Email = dto.Email,
                Name = dto.Name,
                LinkedIn = dto.LinkedIn ?? "",
                Phone = dto.Phone,
            };

            _unitOfWork.Repository<WorkshopBooking>().Add(booking);

            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                throw new Exception("There's an error while add Workshop Booking");

            return new WorkshopBookingDto
            {
                BookingId = booking.Id,
                Email = booking.Email,
                LinkedIn = booking.Email,
                Name = booking.Email,
                Phone = booking.Phone,
            };
        }

        public async Task<bool> DeleteCourseBookingAsync(int bookingId)
        {
            if (bookingId <= 0)
                throw new Exception("Invalid BookingId !!");

            var booking = await _unitOfWork.Repository<CourseBooking>().GetByIdAsync(bookingId);

            if (booking == null)
                throw new Exception("Course Booking Not founded!");

            _unitOfWork.Repository<CourseBooking>().Delete(booking);

            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                throw new Exception("An Error while delete Course Booking Please Call The Backend Developer to solve this problem :)");

            return true;
        }

        public async Task<bool> DeleteWorkshopBookingAsync(int bookingId)
        {
            if (bookingId <= 0)
                throw new Exception("Invalid BookingId !!");

            var booking = await _unitOfWork.Repository<WorkshopBooking>().GetByIdAsync(bookingId);

            if (booking == null)
                throw new Exception("Workshop Booking Not founded!");

            _unitOfWork.Repository<WorkshopBooking>().Delete(booking);

            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                throw new Exception("An Error while delete Course Booking Please Call The Backend Developer to solve this problem :)");

            return true;
        }

        public async Task<int> GetAllBookingCount()
        {
            var specCount = new AllBookingsWithSpecification();

            var totalItems = await _unitOfWork.Repository<CourseBooking>().GetCountAsync(specCount);

            return totalItems;
        }

        public async Task<IReadOnlyList<BookingToReturnDto>> GetAllBookingsAsync(PaginationDto dto)
        {
            var spec = new AllBookingsWithSpecification(dto);

            var booking = await _unitOfWork.Repository<CourseBooking>().GetAllWithSpecAsync(spec);

            if (booking == null || !booking.Any())
                throw new Exception("There's no bookings founded!");

            return booking.Select(b => new BookingToReturnDto
            {
                BookingId = b.Id,
                Address = b.CourseAddress,
                CourseId = b.CourseId,
                CourseName = b.Course.TitleAR,
                Email = b.Email,
                Name = b.Name,
                Phone = b.Phone

            }).ToList().AsReadOnly();

        }

        public async Task<int> GetAllWorkshopBookingCount()
        {
            var spec = new AllWorkshopBookingSpecification();

            var count = await _unitOfWork.Repository<WorkshopBooking>().GetCountAsync(spec);

            return count;
        }

        public async Task<IReadOnlyList<WorkshopBookingDto>> GetAllWorkshopBookingsAsync(PaginationDto dto)
        {
            var spec = new AllWorkshopBookingSpecification(dto);

            var bookings = await _unitOfWork.Repository<WorkshopBooking>().GetAllWithSpecAsync(spec);

            if (bookings == null || !bookings.Any())
                throw new Exception("There'e is no workshop bookings");

            return bookings.Select(b=> new WorkshopBookingDto
            {
                BookingId = b.Id,
                Email = b.Email,
                LinkedIn = b.LinkedIn,
                Name = b.Name,
                Phone = b.Phone

            }).ToList().AsReadOnly();
        }
    }
}
