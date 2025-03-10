using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.DTOs.ContactUs;
using XpertAcademy.Core.DTOs.Pagination;

namespace XpertAcademy.Core.Services
{
    public interface IContactUsService
    {
        Task<ContactUsDto> AddNewAddContactUs(CreateContactUsDto dto);
        Task<IReadOnlyList<ContactUsDto>> GetAllContactUsAsync(PaginationDto dto);
        Task<ContactUsDto> GetContactUsDetailsAsync(int Id);
        Task<int> GetAllContactUsCount();

        Task<bool> DeleteContactUs(int Id);
    }
}
