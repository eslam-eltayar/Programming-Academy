using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.DTOs.Pagination;
using XpertAcademy.Core.DTOs.Review;
using XpertAcademy.Core.Enums;
using XpertAcademy.Core.Models;
using XpertAcademy.Core.Repositories;
using XpertAcademy.Core.Services;
using XpertAcademy.Core.Specifications.Reviews;

namespace XpertAcademy.Service.Services
{
    public class Stud_ReviewService : IStud_ReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileUploadService _fileUploadService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Stud_ReviewService(IUnitOfWork unitOfWork, IFileUploadService fileUploadService, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _fileUploadService = fileUploadService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ReviewToReturnDto> AddNewReview(CreateReviewDto dto)
        {
            if (dto == null)
            {
                throw new Exception("Invalid input. The input cannot be Null!");
            }

            if (dto.image == null || dto.image.Length == 0)
            {
                throw new ArgumentException("Image is required.");
            }


            string imageUrl = await _fileUploadService.UploadFileAsync(dto.image, "reviews");

            var rev = new Stud_Review
            {
                Stud_NameAR = dto.studentNameAR,
                Stud_NameEN = dto.studentNameEN,
                Stud_SM_Link = dto.studentSMLink ?? "",
                CourseId = dto.courseId,
                ReviewAR = dto.reviewAR,
                ReviewEN = dto.reviewEN,
                Stud_ImageUrl = imageUrl,
                
            };

            if (Enum.TryParse<ReviewType>(dto.reviewType, true, out var parsedStatus))
            {
                rev.ReviewType = parsedStatus;
            }
            else
            {
                throw new ArgumentException($"Invalid Review Type : {dto.reviewType}");
            }

            _unitOfWork.Repository<Stud_Review>().Add(rev);

            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
            {
                throw new Exception("There's an Error while saving changes");
            }

            return new ReviewToReturnDto
            {
                ReviewId = rev.Id,
                StudentSMLink = rev.Stud_SM_Link ?? "",
                ReviewAR = rev.ReviewAR,
                ReviewEN = rev.ReviewEN,
                Image = rev.Stud_ImageUrl,
                StudentNameAR = rev.Stud_NameAR,
                StudentNameEN = rev.Stud_NameEN,
                CourseId = rev.CourseId,
                ReviewType = rev.ReviewType.ToString()
                //StudentCourse = "",
            };
        }

        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            if (reviewId <= 0)
                throw new Exception("Invalid Review Id");

            var review = await _unitOfWork.Repository<Stud_Review>().GetByIdAsync(reviewId);

            if (review == null)
                throw new Exception("Review Not founded");

            if (!string.IsNullOrEmpty(review.Stud_ImageUrl))
            {

                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "reviews", review.Stud_ImageUrl);

                imagePath = $"wwwroot{imagePath}";

                if (File.Exists(imagePath))
                    File.Delete(imagePath);

            }

            _unitOfWork.Repository<Stud_Review>().Delete(review);

            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                throw new Exception("There's an error while Delete Review");

            return true;

        }

        public async Task<IReadOnlyList<ReviewToReturnDto>> GetGeneralReviewsAsync()
        {
            var spec = new StudentReviewsSpecifications();

            var reviews = await _unitOfWork.Repository<Stud_Review>().GetAllWithSpecAsync(spec);

            if (reviews == null || !reviews.Any())
                throw new Exception("There's No Reviews Founded!");

            return reviews.Select(rev => new ReviewToReturnDto
            {
                ReviewId = rev.Id,
                StudentSMLink = rev.Stud_SM_Link ?? "",
                ReviewAR = rev.ReviewAR,
                ReviewEN = rev.ReviewEN,
                Image = rev.Stud_ImageUrl,
                StudentNameAR = rev.Stud_NameAR,
                StudentNameEN = rev.Stud_NameEN,
                CourseId = rev.CourseId,
                ReviewType = rev.ReviewType.ToString()
                //StudentCourse = rev.Course.TitleAR ?? "",

            }).ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<ReviewToReturnDto>> GetAllReviewsForCourseAsync(int courseId)
        {
            var spec = new StudentReviewsSpecifications(courseId);

            var reviews = await _unitOfWork.Repository<Stud_Review>().GetAllWithSpecAsync(spec);

            if (reviews == null || !reviews.Any())
                throw new Exception("There's No Reviews Founded!");

            return reviews.Select(rev => new ReviewToReturnDto
            {
                ReviewId = rev.Id,
                StudentSMLink = rev.Stud_SM_Link ?? "",
                ReviewAR = rev.ReviewAR,
                ReviewEN = rev.ReviewEN,
                Image = rev.Stud_ImageUrl,
                StudentNameAR = rev.Stud_NameAR,
                StudentNameEN = rev.Stud_NameEN,
                CourseId = rev.CourseId,
                ReviewType = rev.ReviewType.ToString()
                //StudentCourse = rev.Course.TitleAR ?? "",

            }).ToList().AsReadOnly();
        }

        public async Task<ReviewToReturnDto> UpdateReviewAsync(int reviewId, CreateReviewDto dto)
        {
            if (reviewId <= 0)
                throw new Exception("Invalid Review Id");

            var review = await _unitOfWork.Repository<Stud_Review>().GetByIdAsync(reviewId);

            if (review == null)
                throw new Exception("Review Not found ya lifaa");

            if (dto == null)
            {
                throw new Exception("Invalid input. The input cannot be Null!");
            }

            if (dto.image == null || dto.image.Length == 0)
            {
                throw new ArgumentException("Image is required.");
            }

            review.Stud_SM_Link = dto.studentSMLink;
            review.ReviewAR = dto.reviewAR;
            review.ReviewEN = dto.reviewEN;
            review.Stud_NameEN = dto.studentNameEN;
            review.Stud_NameAR = dto.studentNameAR;
            review.CourseId = dto.courseId;

            if (Enum.TryParse<ReviewType>(dto.reviewType, true, out var parsedStatus))
            {
                review.ReviewType = parsedStatus;
            }
            else
            {
                throw new ArgumentException($"Invalid Review Type : {dto.reviewType}");
            }

            if (!string.IsNullOrEmpty(review.Stud_ImageUrl))
            {

                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "reviews", review.Stud_ImageUrl);

                imagePath = $"wwwroot{imagePath}";

                if (File.Exists(imagePath))
                    File.Delete(imagePath);

            }


            var newImageUrl = await _fileUploadService.UploadFileAsync(dto.image, "reviews");

            review.Stud_ImageUrl = newImageUrl;

            _unitOfWork.Repository<Stud_Review>().Update(review);

            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                throw new Exception("There's an Error while Update Review Call Backend Developer");

            return new ReviewToReturnDto
            {
                ReviewId = review.Id,
                StudentSMLink = review.Stud_SM_Link ?? "",
                ReviewAR = review.ReviewAR,
                ReviewEN = review.ReviewEN,
                Image = review.Stud_ImageUrl,
                StudentNameAR = review.Stud_NameAR,
                StudentNameEN = review.Stud_NameEN,
                CourseId = review.CourseId,
                //StudentCourse = "",
            };
        }

        public async Task<IReadOnlyList<ReviewToReturnDto>> GetAllReviewsAsync(PaginationDto dto)
        {
            var spec = new StudentReviewsSpecifications(dto);

            var reviews = await _unitOfWork.Repository<Stud_Review>().GetAllWithSpecAsync(spec);

            if (reviews == null || !reviews.Any())
                throw new Exception("There's No Reviews Founded!");

            return reviews.Select(rev => new ReviewToReturnDto
            {
                ReviewId = rev.Id,
                StudentSMLink = rev.Stud_SM_Link ?? "",
                ReviewAR = rev.ReviewAR,
                ReviewEN = rev.ReviewEN,
                Image = rev.Stud_ImageUrl,
                StudentNameAR = rev.Stud_NameAR,
                StudentNameEN = rev.Stud_NameEN,
                CourseId = rev.CourseId,
                ReviewType = rev.ReviewType.ToString()
                //StudentCourse = rev.Course.TitleAR ?? "",

            }).ToList().AsReadOnly();
        }

        public async Task<int> GetAllReviewsCount()
        {
            var spec = new AllReviewsForCountSpecification();

            int count = await _unitOfWork.Repository<Stud_Review>().GetCountAsync(spec);

            return count;
        }
    }
}
