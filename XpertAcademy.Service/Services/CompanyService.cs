using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.DTOs.Company;
using XpertAcademy.Core.DTOs.Pagination;
using XpertAcademy.Core.Models;
using XpertAcademy.Core.Repositories;
using XpertAcademy.Core.Services;
using XpertAcademy.Core.Specifications.Companies;

namespace XpertAcademy.Service.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileUploadService _fileUploadService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CompanyService(IUnitOfWork unitOfWork, IFileUploadService fileUploadService, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _fileUploadService = fileUploadService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<CompanyToReturnDto> AddNewCompanyAsync(CreateCompanyDto dto)
        {
            if (dto == null)
                throw new Exception("Invalid input. The input cannot be null!");

            if (dto.logo == null || dto.logo.Length == 0)
            {
                throw new ArgumentException("Image is required.");
            }

            string imageUrl = await _fileUploadService.UploadFileAsync(dto.logo, "companies");

            var company = new Company
            {
                Logo = imageUrl,
                NameAR = dto.nameAR,
                NameEN = dto.nameEN,

            };

            _unitOfWork.Repository<Company>().Add(company);

            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                throw new Exception("There's an error while adding company ya Lifaaa");

            return new CompanyToReturnDto
            {
                CompanyId = company.Id,
                Logo = company.Logo,
                NameAR = company.NameAR,
                NameEN = company.NameEN
            };
        }

        public async Task<bool> DeleteCompanyAsync(int companyId)
        {
            if (companyId <= 0)
                throw new Exception("Invalid Company Id");

            var company = await _unitOfWork.Repository<Company>().GetByIdAsync(companyId);

            if (company == null)
                throw new Exception("Company not founded");

            if (!string.IsNullOrEmpty(company.Logo))
            {

                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "companies", company.Logo);

                imagePath = $"wwwroot{imagePath}";

                if (File.Exists(imagePath))
                    File.Delete(imagePath);

            }

            _unitOfWork.Repository<Company>().Delete(company);

            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                throw new Exception("There's an error while Deleting company ya Lifaaa");

            return true;
        }

        public async Task<IReadOnlyList<CompanyToReturnDto>> GetAllCompaniesAsync(PaginationDto dto)
        {
            var spec = new AllCompaniesSpecifcation(dto);

            var companies = await _unitOfWork.Repository<Company>().GetAllWithSpecAsync(spec);

            if (companies == null || !companies.Any())
                throw new Exception("There's No Companies to return !");

            return companies.Select(com => new CompanyToReturnDto
            {
                CompanyId = com.Id,
                Logo = com.Logo,
                NameAR = com.NameAR,
                NameEN = com.NameEN

            }).ToList().AsReadOnly();
        }

        public async Task<int> GetAllCompaniesCount()
        {
            var spec = new AllCompaniesSpecifcation();

            var count = await _unitOfWork.Repository<Company>().GetCountAsync(spec);

            return count;
        }

        public async Task<CompanyToReturnDto> GetCompanyDetailsAsync(int companyId)
        {
            if (companyId <= 0)
                throw new Exception("Invalid companyId");

            var company = await _unitOfWork.Repository<Company>().GetByIdAsync(companyId);

            if (company == null)
                throw new Exception("Company Not founded!");

            return new CompanyToReturnDto
            {
                CompanyId = company.Id,
                Logo = company.Logo,
                NameAR = company.NameAR,
                NameEN = company.NameEN
            };
        }

        public async Task<CompanyToReturnDto> UpdateCompanyAsync(int companyId, CreateCompanyDto dto)
        {
            if (companyId <= 0)
                throw new Exception("Invalid Company Id!");

            var company = await _unitOfWork.Repository<Company>().GetByIdAsync(companyId);

            if (company == null)
                throw new Exception("The company Not Founded!");

            if (dto == null)
                throw new Exception("Invalid input. The input cannot be null!");

            if (dto.logo == null || dto.logo.Length == 0)
            {
                throw new ArgumentException("Image is required.");
            }

            company.NameAR = dto.nameAR;
            company.NameEN = dto.nameEN;

            if (!string.IsNullOrEmpty(company.Logo))
            {

                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "companies", company.Logo);

                imagePath = $"wwwroot{imagePath}";

                if (File.Exists(imagePath))
                    File.Delete(imagePath);

            }


            var newLogo = await _fileUploadService.UploadFileAsync(dto.logo, "companies");

            company.Logo = newLogo;

            _unitOfWork.Repository<Company>().Update(company);

            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                throw new Exception("There's an error while update Company!");

            return new CompanyToReturnDto
            {
                CompanyId = company.Id,
                Logo = company.Logo,
                NameAR = company.NameAR,
                NameEN = company.NameEN
            };

        }
    }
}
