using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.DTOs.Course;
using XpertAcademy.Core.DTOs.Pagination;

namespace XpertAcademy.Core.Services
{
    public interface ICourseService
    {
        Task<CourseToReturnDto> AddNewCourse(CreateCourseDto dto, List<AddContentDto> contents);
        Task<CourseToReturnDto> GetCourseDetailsAsync(int id);
        Task<IReadOnlyList<AllCoursesToReturnDto>> GetAllCoursesAsync(PaginationDto paginationDto);
        Task<int> GetAllCoursesCount();
        Task<CourseToReturnDto> UpdateCourseAsync(int courseId, CreateCourseDto dto);
        Task<bool> DeleteCourseAsync(int courseId);
    }
}
