using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.DTOs.Course;
using XpertAcademy.Core.DTOs.Pagination;
using XpertAcademy.Core.Enums;
using XpertAcademy.Core.Models;
using XpertAcademy.Core.Repositories;
using XpertAcademy.Core.Services;
using XpertAcademy.Core.Specifications.CourseContents;
using XpertAcademy.Core.Specifications.Courses;
using XpertAcademy.Core.Specifications.Projects;


namespace XpertAcademy.Service.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileUploadService _fileUploadService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CourseService(IUnitOfWork unitOfWork, IFileUploadService fileUploadService, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _fileUploadService = fileUploadService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<CourseToReturnDto> AddNewCourse(CreateCourseDto dto, List<AddContentDto> contents)
        {
            if (dto == null)
            {
                throw new Exception("Invalid input. The input cannot be Null!");
            }

            if (dto.Image == null || dto.Image.Length == 0)
            {
                throw new ArgumentException("Image is required.");
            }

            string imageUrl = await _fileUploadService.UploadFileAsync(dto.Image, "courses");

            var course = new Course
            {
                TitleAR = dto.TitleAR,
                ExternalDescriptionAR = dto.ExternalDescriptionAR,
                Price = dto.Price,
                ImageUrl = imageUrl,
                Duration = dto.Duration,
                TitleEN = dto.TitleEN,
                ExternalDescriptionEN = dto.ExternalDescriptionEN,
                InternalDescriptionAR = dto.InternalDescriptionAR,
                InternalDescriptionEN = dto.InternalDescriptionEN,
                

            };

            if (Enum.TryParse<CourseStatus>(dto.CourseStatus, true, out var parsedStatus))
            {
                course.CourseStatus = parsedStatus;
            }
            else
            {
                throw new ArgumentException($"Invalid Course Status value: {dto.CourseStatus}");
            }

            _unitOfWork.Repository<Course>().Add(course);


            int addCourseResult = await _unitOfWork.CompleteAsync();

            if (addCourseResult <= 0)
                throw new Exception("There's an Error while Add Course");

            foreach (var content in contents)
            {
                var courseContent = new CourseContent
                {
                    CourseId = course.Id,
                    TitleAR = content.TitleAR,
                    TitleEN = content.TitleEN,
                    DescriptionAR = content.DescriptionAR,
                    DescriptionEN = content.DescriptionEN
                };

                _unitOfWork.Repository<CourseContent>().Add(courseContent);
            }
            int addContentResult = await _unitOfWork.CompleteAsync();

            if (addContentResult <= 0)
            {
                throw new Exception("There's an Error while saving Course Content!");
            }

            foreach (var video in dto.Videos)
            {
                var courseVideos = new Stud_Projects
                {
                    CourseId = course.Id,
                    Project_Link = video.Link ?? "",
                };

                _unitOfWork.Repository<Stud_Projects>().Add(courseVideos);
            }

            int addVideoResult = await _unitOfWork.CompleteAsync();

            if (addVideoResult <= 0)
            {
                throw new Exception("There's an Error while saving Video!");
            }

            return new CourseToReturnDto
            {
                CourseId = course.Id,

                ImageUrl = course.ImageUrl,
                Price = course.Price,

                Duration = course.Duration,
                PriceAfterDiscount = course.PriceAfterDiscount
            };
        }

        public async Task<bool> DeleteCourseAsync(int courseId)
        {
            if (courseId <= 0)
                throw new Exception("Invalid Id!");

            var course = await _unitOfWork.Repository<Course>().GetByIdAsync(courseId);

            if (course == null)
                throw new Exception("Course Not Founded!");

            if (!string.IsNullOrEmpty(course.ImageUrl))
            {

                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "courses", course.ImageUrl);

                imagePath = $"wwwroot{imagePath}";

                if (File.Exists(imagePath))
                    File.Delete(imagePath);

            }

            _unitOfWork.Repository<Course>().Delete(course);

            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                throw new Exception("An error occurred while saving changes to the database.");


            return true;
        }

        public async Task<IReadOnlyList<AllCoursesToReturnDto>> GetAllCoursesAsync(PaginationDto paginationDto)
        {
            var spec = new AllCourseSpecifications(paginationDto);

            var courses = await _unitOfWork.Repository<Course>().GetAllWithSpecAsync(spec);

            if (!courses.Any() || courses == null)
                throw new Exception("No Courses Founded!");

            return courses.Select(course => new AllCoursesToReturnDto
            {
                CourseId = course.Id,
                TitleAR = course.TitleAR,
                TitleEN = course.TitleEN,
                ImageUrl = course.ImageUrl,
                ExternalDescriptionAR = course.ExternalDescriptionAR,
                ExternalDescriptionEN = course.ExternalDescriptionEN,
                Price = course.Price,
                CourseStatus = course.CourseStatus.ToString(),
                PriceAfterDiscount = course.PriceAfterDiscount,
                Duration = course.Duration

            }).ToList();
        }

        public async Task<int> GetAllCoursesCount()
        {
            var specCount = new AllCourseSpecifications();

            var totalItems = await _unitOfWork.Repository<Course>().GetCountAsync(specCount);

            return totalItems;
        }

        public async Task<CourseToReturnDto> GetCourseDetailsAsync(int id)
        {
            var course = await _unitOfWork.Repository<Course>().GetByIdAsync(id);

            if (course == null)
            {
                throw new Exception("Course Not Found");
            }

            var contentSpec = new AllCourseContentSpecifications(course.Id);

            var videosSpec = new ProjectsOfCourseSpecification(course.Id);


            var contents = await _unitOfWork.Repository<CourseContent>().GetAllWithSpecAsync(contentSpec);

            var videos = await _unitOfWork.Repository<Stud_Projects>().GetAllWithSpecAsync(videosSpec);


            return new CourseToReturnDto
            {
                CourseId = course.Id,
                TitleAR = course.TitleAR,
                TitleEN = course.TitleEN,
                Price = course.Price,
                PriceAfterDiscount = course.PriceAfterDiscount,
                CourseStatus = course.CourseStatus.ToString(),
                Duration = course.Duration,
                ExternalDescriptionAR = course.ExternalDescriptionAR,
                ExternalDescriptionEN = course.ExternalDescriptionEN,
                ImageUrl = course.ImageUrl,
                InternalDescriptionAR = course.InternalDescriptionAR,
                InternalDescriptionEN = course.InternalDescriptionEN,


                Contents = contents.Select(c => new AddContentDto
                {
                    TitleAR = c.TitleAR,
                    TitleEN = c.TitleEN,
                    DescriptionAR = c.DescriptionAR,
                    DescriptionEN = c.DescriptionEN

                }).ToList(),


                Videos = videos.Select(v => new AddVideotDto
                {
                    Link = v.Project_Link

                }).ToList()

            };
        }

        public async Task<CourseToReturnDto> UpdateCourseAsync(int courseId, CreateCourseDto dto)
        {
            if (courseId <= 0)
                throw new Exception($"Invalid courseId {courseId}");

            var course = await _unitOfWork.Repository<Course>().GetByIdAsync(courseId);

            if (course == null)
                throw new Exception("Course Not Founded!");

            // Update course properties

            course.TitleAR = dto.TitleAR;
            course.TitleEN = dto.TitleEN;
            course.ExternalDescriptionAR = dto.ExternalDescriptionAR;
            course.ExternalDescriptionEN = dto.ExternalDescriptionEN;
            course.InternalDescriptionAR = dto.InternalDescriptionAR;
            course.InternalDescriptionEN = dto.InternalDescriptionEN;
            course.Price = dto.Price;
            course.Duration = dto.Duration;

            if (Enum.TryParse<CourseStatus>(dto.CourseStatus, true, out var parsedStatus))
            {
                course.CourseStatus = parsedStatus;
            }
            else
            {
                throw new ArgumentException($"Invalid CourseStatus value: {dto.CourseStatus}");
            }

            if (!string.IsNullOrEmpty(course.ImageUrl))
            {

                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "courses", course.ImageUrl);

                imagePath = $"wwwroot{imagePath}";

                if (File.Exists(imagePath))
                    File.Delete(imagePath);

            }


            var newImageUrl = await _fileUploadService.UploadFileAsync(dto.Image, "courses");

            course.ImageUrl = newImageUrl;

            if (dto.Contents != null && dto.Contents.Count > 0)
            {

                course.CourseContents.Clear();

                foreach (var content in dto.Contents)
                {
                    var courseContent = new CourseContent
                    {
                        CourseId = course.Id,
                        TitleAR = content.TitleAR,
                        TitleEN = content.TitleEN,
                        DescriptionAR = content.DescriptionAR,
                        DescriptionEN = content.DescriptionEN
                    };
                    course.CourseContents.Add(courseContent);
                }
            }

            if (dto.Videos != null && dto.Videos.Count > 0)
            {
                course.Stud_Projects.Clear();

                foreach (var video in dto.Videos)
                {
                    var courseVideos = new Stud_Projects
                    {
                        CourseId = course.Id,
                        Project_Link = video.Link ?? "",
                    };

                    _unitOfWork.Repository<Stud_Projects>().Add(courseVideos);
                }
            }

            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
            {
                throw new Exception("There's an Error while saving Course Content!");
            }

            return new CourseToReturnDto
            {
                CourseId = course.Id,
                ImageUrl = course.ImageUrl,
                Price = course.Price,
                Duration = course.Duration,
                PriceAfterDiscount = course.PriceAfterDiscount
            };

        }
    }
}
