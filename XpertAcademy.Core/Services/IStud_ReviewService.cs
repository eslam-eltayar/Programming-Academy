using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.DTOs.Pagination;
using XpertAcademy.Core.DTOs.Review;

namespace XpertAcademy.Core.Services
{
    public interface IStud_ReviewService
    {
        Task<ReviewToReturnDto> AddNewReview(CreateReviewDto dto);
        Task<IReadOnlyList<ReviewToReturnDto>> GetGeneralReviewsAsync();
        Task<IReadOnlyList<ReviewToReturnDto>> GetAllReviewsAsync(PaginationDto dto);
        Task<IReadOnlyList<ReviewToReturnDto>> GetAllReviewsForCourseAsync(int courseId);
        Task<int> GetAllReviewsCount();
        Task<ReviewToReturnDto> UpdateReviewAsync(int reviewId, CreateReviewDto dto);
        Task<bool> DeleteReviewAsync(int reviewId);
    }
}
