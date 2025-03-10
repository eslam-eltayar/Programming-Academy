using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.DTOs.Company;
using XpertAcademy.Core.DTOs.Pagination;

namespace XpertAcademy.Core.Services
{
    public interface ICompanyService
    {
        Task<CompanyToReturnDto> AddNewCompanyAsync(CreateCompanyDto dto);
        Task<IReadOnlyList<CompanyToReturnDto>> GetAllCompaniesAsync(PaginationDto dto);
        Task<CompanyToReturnDto> GetCompanyDetailsAsync(int companyId);
        Task<int> GetAllCompaniesCount();
        Task<CompanyToReturnDto> UpdateCompanyAsync(int companyId , CreateCompanyDto dto);
        Task<bool> DeleteCompanyAsync(int companyId);
    }
}
