using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.DTOs.ContactUs;
using XpertAcademy.Core.DTOs.Pagination;
using XpertAcademy.Core.Models;
using XpertAcademy.Core.Repositories;
using XpertAcademy.Core.Services;
using XpertAcademy.Core.Specifications.ContactUss;
using static System.Net.Mime.MediaTypeNames;

namespace XpertAcademy.Service.Services
{
    public class ContactUsService : IContactUsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactUsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ContactUsDto> AddNewAddContactUs(CreateContactUsDto dto)
        {
            if (dto == null)
            {
                throw new Exception("Invalid input. The input cannot be Null!");
            }

            var cont = new ContactUs
            {
                Body = dto.Body,
                Email = dto.Email,
                Name = dto.Name,
                Subject = dto.Subject,
                Date = DateTime.Now.Date

            };

            _unitOfWork.Repository<ContactUs>().Add(cont);

            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
            {
                throw new Exception("There's an Error while saving changes");
            }

            return new ContactUsDto
            {
                Id = cont.Id,
                Subject = cont.Subject,
                Body = cont.Body,
                Email = cont.Email,
                Name = cont.Name,
                Date = cont.Date.ToString("yyyy-MM-dd")
            };
        }

        public async Task<bool> DeleteContactUs(int Id)
        {
            if (Id <= 0)
                throw new Exception("Invalid Id");

            var contactUs = await _unitOfWork.Repository<ContactUs>().GetByIdAsync(Id);

            if (contactUs == null)
                throw new Exception("Contact Us Not founded!");


            _unitOfWork.Repository<ContactUs>().Delete(contactUs);

            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
            {
                throw new Exception("There's an Error while Delete ContactUs");
            }

            return true;

        }

        public async Task<IReadOnlyList<ContactUsDto>> GetAllContactUsAsync(PaginationDto dto)
        {
            var spec = new AllContactUsSpecification(dto);

            var contacts = await _unitOfWork.Repository<ContactUs>().GetAllWithSpecAsync(spec);

            if (contacts == null || !contacts.Any())
            {
                throw new Exception("There's No Contacts Found!");
            }

            return contacts.Select(con => new ContactUsDto
            {
                Id = con.Id,
                Body = con.Body,
                Email = con.Email,
                Name = con.Name,
                Subject = con.Subject,
                Date = con.Date.ToString("yyyy-MM-dd")

            }).ToList().AsReadOnly();
        }

        public async Task<int> GetAllContactUsCount()
        {
            var spec = new AllContactUsSpecification();

            var count = await _unitOfWork.Repository<ContactUs>().GetCountAsync(spec);

            return count;
        }

        public async Task<ContactUsDto> GetContactUsDetailsAsync(int Id)
        {
            if (Id <= 0)
                throw new Exception("Invalid Id");

            var contactUs = await _unitOfWork.Repository<ContactUs>().GetByIdAsync(Id);

            if (contactUs == null)
                throw new Exception("Contact Us Not Founded!");

            return new ContactUsDto
            {
                Id = contactUs.Id,
                Subject = contactUs.Subject,
                Body = contactUs.Body,
                Email = contactUs.Email,
                Name = contactUs.Name,
                Date = contactUs.Date.ToString("yyyy-MM-dd")
            };
        }
    }
}
