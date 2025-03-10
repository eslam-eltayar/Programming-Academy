using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.DTOs.Pagination;
using XpertAcademy.Core.DTOs.Workshop;
using XpertAcademy.Core.Enums;
using XpertAcademy.Core.Models;
using XpertAcademy.Core.Repositories;
using XpertAcademy.Core.Services;
using XpertAcademy.Core.Specifications.Workshops;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace XpertAcademy.Service.Services
{
    public class WorkshopService : IWorkshopService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileUploadService _fileUploadService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public WorkshopService(IUnitOfWork unitOfWork, IFileUploadService fileUploadService, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _fileUploadService = fileUploadService;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<WorkshopToReturnDto> AddNewWorkshopAsync(CreateWorkshopDto dto)
        {
            if (dto == null)
                throw new Exception("Invalid Input. The body cannot be null");

            if (dto.Image == null || dto.Image.Length == 0)
            {
                throw new ArgumentException("Image is required.");
            }

            string imageUrl = await _fileUploadService.UploadFileAsync(dto.Image, "workshops");

            var work = new Workshop
            {
                TitleAR = dto.TitleAR,
                TitleEN = dto.TitleEN,
                DescriptionAR = dto.DescriptionAR,
                DescriptionEN = dto.DescriptionEN,
                Image = imageUrl,
                Date = dto.Date

            };

            if (dto.Price > 0 && dto.Price != null)
            {
                work.Price = (decimal)dto.Price;
            }

            if (Enum.TryParse<WorkshopType>(dto.Type, true, out var parsedStatus))
            {
                work.Type = parsedStatus;
            }
            else
            {
                throw new ArgumentException($"Invalid Workshop Type: {dto.Type}");
            }

            _unitOfWork.Repository<Workshop>().Add(work);

            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                throw new Exception("There's an error while adding Workshop!");

            foreach (var audiance in dto.WorkshopAudiences)
            {
                var workshopAudiance = new WorkshopAudience
                {
                    WorkshopId = work.Id,
                    TitleAR = audiance.TitleAR,
                    TitleEN = audiance.TitleEN,
                    DescriptionAR = audiance.DescriptionAR,
                    DescriptionEN = audiance.DescriptionEN,

                };

                _unitOfWork.Repository<WorkshopAudience>().Add(workshopAudiance);
            }

            int addWorkshopAudience = await _unitOfWork.CompleteAsync();

            if (addWorkshopAudience <= 0)
            {
                throw new Exception("There's an Error while add Workshop Audience!");
            }

            return new WorkshopToReturnDto
            {
                WorkshopId = work.Id,
                Date = work.Date.ToString(),
                DescriptionAR = work.DescriptionAR,
                DescriptionEN = work.DescriptionEN,
                Image = work.Image,
                TitleAR = work.TitleAR,
                TitleEN = work.TitleEN,
                Type = work.Type.ToString()
            };

        }

        public async Task<bool> DeleteWorkshopAsync(int workshopId)
        {
            if (workshopId <= 0)
                throw new Exception("Invalid workshop Id");

            var workshop = await _unitOfWork.Repository<Workshop>().GetByIdAsync(workshopId);

            if (workshop == null)
                throw new Exception("Workshop not founded");

            if (!string.IsNullOrEmpty(workshop.Image))
            {

                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "workshops", workshop.Image);

                imagePath = $"wwwroot{imagePath}";

                if (File.Exists(imagePath))
                    File.Delete(imagePath);

            }

            _unitOfWork.Repository<Workshop>().Delete(workshop);

            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                throw new Exception("There's an error while Deleting Workshop ya Lifaaa");

            return true;
        }

        public async Task<WorkshopToReturnDto> EditWorkshopAsync(int workshopId, CreateWorkshopDto dto)
        {
            if (workshopId <= 0)
                throw new Exception("Invalid workshop Id");

            var workshop = await _unitOfWork.Repository<Workshop>().GetByIdAsync(workshopId);

            if (workshop == null)
                throw new Exception("Workshop not founded!");

            if (dto == null)
                throw new Exception("Invalid input. The input cannot be null!");

            if (dto.Image == null || dto.Image.Length == 0)
            {
                throw new ArgumentException("Image is required.");
            }

            workshop.TitleAR = dto.TitleAR;
            workshop.TitleEN = dto.TitleEN;
            workshop.DescriptionAR = dto.DescriptionAR;
            workshop.DescriptionEN = dto.DescriptionEN;
            workshop.Date = dto.Date;

            if (dto.Price > 0 && dto.Price != null)
            {
                workshop.Price = (decimal)dto.Price;
            }

            if (Enum.TryParse<WorkshopType>(dto.Type, true, out var parsedStatus))
                workshop.Type = parsedStatus;
            else
                throw new ArgumentException($"Invalid Workshop Type: {dto.Type}");


            if (!string.IsNullOrEmpty(workshop.Image))
            {

                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "workshops", workshop.Image);

                imagePath = $"wwwroot{imagePath}";

                if (File.Exists(imagePath))
                    File.Delete(imagePath);

            }

            var newImage = await _fileUploadService.UploadFileAsync(dto.Image, "workshops");

            workshop.Image = newImage;

            if (dto.WorkshopAudiences != null && dto.WorkshopAudiences.Count > 0)
            {

                workshop.WorkshopAudiences.Clear();

                foreach (var audiance in dto.WorkshopAudiences)
                {
                    var workshopAudiance = new WorkshopAudience
                    {
                        WorkshopId = workshop.Id,
                        TitleAR = audiance.TitleAR,
                        TitleEN = audiance.TitleEN,
                        DescriptionAR = audiance.DescriptionAR,
                        DescriptionEN = audiance.DescriptionEN,

                    };

                    _unitOfWork.Repository<WorkshopAudience>().Add(workshopAudiance);
                }
            }

            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                throw new Exception("There's an error while Updating Workshop!");


            return new WorkshopToReturnDto
            {
                WorkshopId = workshop.Id,
                Date = workshop.Date.ToString(),
                DescriptionAR = workshop.DescriptionAR,
                DescriptionEN = workshop.DescriptionEN,
                Image = workshop.Image,
                TitleAR = workshop.TitleAR,
                TitleEN = workshop.TitleEN,
                Type = workshop.Type.ToString()
            };
        }

        public async Task<IReadOnlyList<WorkshopToReturnDto>> GetAllWorkshopsAsync(PaginationDto dto)
        {
            var spec = new AllWorkshopsSpecification(dto);

            var workshops = await _unitOfWork.Repository<Workshop>().GetAllWithSpecAsync(spec);

            if (workshops == null || !workshops.Any())
            {
                throw new Exception("There's no Workshops to return");
            }

            CultureInfo arabicCulture = new CultureInfo("ar");
            CultureInfo englishCulture = new CultureInfo("en-US");


            return workshops.Select(work => new WorkshopToReturnDto
            {

                WorkshopId = work.Id,
                TitleAR = work.TitleAR,
                TitleEN = work.TitleEN,

                Date = work.Date.ToString("dddd d MMMM yyyy الساعة h:mm tt", arabicCulture)
                 .Replace("AM", "صباحاً")
                 .Replace("PM", "مساءً"),


                DateEN = work.Date.ToString("dddd d MMMM yyyy h:mm tt", englishCulture)
                        .Replace("AM", "AM")
                        .Replace("PM", "PM"),

                DescriptionAR = work.DescriptionAR,
                DescriptionEN = work.DescriptionEN,
                Image = work.Image,
                Type = work.Type.ToString(),
                Price = work.Price

            }).ToList().AsReadOnly();
        }

        public async Task<int> GetAllWorkshopsCount()
        {
            var spec = new AllWorkshopsSpecification();

            var count = await _unitOfWork.Repository<Workshop>().GetCountAsync(spec);

            return count;
        }

        public async Task<WorkshopToReturnDto> GetWorkshopDetailsAsync(int workshopId)
        {
            if (workshopId <= 0)
                throw new Exception("Invalid workshop id");

            var work = await _unitOfWork.Repository<Workshop>().GetByIdAsync(workshopId);

            if (work == null)
                throw new Exception("workshop not founded!!");

            var audienceSpec = new AllWorkshopAudiencesSpecification(work.Id);

            var audiences = await _unitOfWork.Repository<WorkshopAudience>().GetAllWithSpecAsync(audienceSpec);

            CultureInfo arabicCulture = new CultureInfo("ar");
            CultureInfo englishCulture = new CultureInfo("en-US");

            string formattedDate = work.Date.ToString("dddd d MMMM yyyy الساعة h tt", arabicCulture)
                                           .Replace("AM", "صباحاً")
                                           .Replace("PM", "مساءً")
                                           .Replace("ص", "صباحاً")
                                           .Replace("م", "مساءً");

            return new WorkshopToReturnDto
            {
                WorkshopId = work.Id,
                TitleAR = work.TitleAR,
                TitleEN = work.TitleEN,
                Date = formattedDate,
                DescriptionAR = work.DescriptionAR,
                DescriptionEN = work.DescriptionEN,
                Image = work.Image,
                Type = work.Type.ToString(),

                DateEN = work.Date.ToString("dddd d MMMM yyyy h:mm tt", englishCulture)
                        .Replace("AM", "AM")
                        .Replace("PM", "PM"),

                WorkshopAudiences = audiences.Select(a => new WorkshopAudienceDto
                {
                    TitleAR = a.TitleAR,
                    TitleEN = a.TitleEN,
                    DescriptionAR = a.DescriptionAR,
                    DescriptionEN = a.DescriptionEN

                }).ToList().AsReadOnly(),
                Price = work.Price
            };
        }
    }
}
