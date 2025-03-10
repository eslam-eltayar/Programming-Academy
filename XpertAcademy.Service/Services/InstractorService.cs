using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.DTOs.Instractor;
using XpertAcademy.Core.DTOs.Pagination;
using XpertAcademy.Core.Models;
using XpertAcademy.Core.Repositories;
using XpertAcademy.Core.Services;
using XpertAcademy.Core.Specifications.Courses;
using XpertAcademy.Core.Specifications.Instractos;
using XpertAcademy.Reposatories.Repositories;

namespace XpertAcademy.Service.Services
{
    public class InstractorService : IInstractorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileUploadService _fileUploadService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public InstractorService(IUnitOfWork unitOfWork, IFileUploadService fileUploadService, IWebHostEnvironment webHostEnvironment)
        {

            _unitOfWork = unitOfWork;
            _fileUploadService = fileUploadService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IReadOnlyList<InstractorToReturnDto>> GetAllInstractorsAsync(PaginationDto dto)
        {
            var spec = new AllInstractorSpecification(dto);

            var instractors = await _unitOfWork.Repository<Instractor>().GetAllWithSpecAsync(spec);

            if (!instractors.Any() || instractors == null)
            {
                throw new Exception("There's No instractors founded !");
            }

            return instractors.Select(ins => new InstractorToReturnDto
            {
                InstractorId = ins.Id,
                Image = ins.ImageUrl,
                AboutEN = ins.AboutEN,
                AboutAR = ins.AboutAR,
                NameEN = ins.NameEN,
                NameAR = ins.NameAR

            }).ToList().AsReadOnly();
        }
        public async Task<InstractorToReturnDto> AddNewInstractor(CreateInstractorDto dto)
        {
            if (dto == null)
            {
                throw new Exception("Invalid input. The input cannot be Null!");
            }

            if (dto.image == null || dto.image.Length == 0)
            {
                throw new ArgumentException("Image is required.");
            }

            string imageUrl = await _fileUploadService.UploadFileAsync(dto.image, "instractors");

            var ins = new Instractor
            {
                NameAR = dto.nameAR,
                NameEN = dto.nameEN,
                AboutAR = dto.aboutAR,
                AboutEN = dto.aboutEN,
                ImageUrl = imageUrl
            };

            _unitOfWork.Repository<Instractor>().Add(ins);

            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
            {
                throw new Exception("There's an Error while saving changes");
            }

            return new InstractorToReturnDto
            {
                InstractorId = ins.Id,
                NameAR = ins.NameAR,
                NameEN = ins.NameEN,
                AboutAR = ins.AboutAR,
                AboutEN = ins.AboutEN,
                Image = ins.ImageUrl
            };
        }

        public async Task<InstractorToReturnDto> UpdateInstractorAsync(int instractorId, CreateInstractorDto dto)
        {
            var instractor = await _unitOfWork.Repository<Instractor>().GetByIdAsync(instractorId);

            if (instractor == null)
                throw new Exception("Instracto not founded!");

            instractor.NameAR = dto.nameAR;
            instractor.NameEN = dto.nameEN;
            instractor.AboutAR = dto.aboutAR;
            instractor.AboutEN = dto.aboutEN;

            if (!string.IsNullOrEmpty(instractor.ImageUrl))
            {

                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "instractors", instractor.ImageUrl);

                imagePath = $"wwwroot{imagePath}";

                if (File.Exists(imagePath))
                    File.Delete(imagePath);

            }


            var newImageUrl = await _fileUploadService.UploadFileAsync(dto.image, "instractors");

            instractor.ImageUrl = newImageUrl;

            _unitOfWork.Repository<Instractor>().Update(instractor);

            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                throw new Exception("There's an Error while Update Instractor Data!");

            return new InstractorToReturnDto
            {
                InstractorId = instractor.Id,
                NameAR = instractor.NameAR,
                NameEN = instractor.NameEN,
                AboutAR = instractor.AboutAR,
                AboutEN = instractor.AboutEN,
                Image = instractor.ImageUrl
            };
        }

        public async Task<int> GetInstractorsCount()
        {
            var specCount = new AllInstractorSpecification();

            var totalItems = await _unitOfWork.Repository<Instractor>().GetCountAsync(specCount);

            return totalItems;
        }

        public async Task<bool> DeleteInstractorAsync(int instractorId)
        {
            var instractor = await _unitOfWork.Repository<Instractor>().GetByIdAsync(instractorId);

            if (instractor == null)
                throw new Exception("Instractor Not founded!");

            if (!string.IsNullOrEmpty(instractor.ImageUrl))
            {

                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "instractors", instractor.ImageUrl);

                imagePath = $"wwwroot{imagePath}";

                if (File.Exists(imagePath))
                    File.Delete(imagePath);

            }

            _unitOfWork.Repository<Instractor>().Delete(instractor);

            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                throw new Exception("There's an Error while Delete Instractor!");

            return true;

        }
    }
}
