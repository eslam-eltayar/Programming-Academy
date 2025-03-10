using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XpertAcademy.APIs.Helpers;
using XpertAcademy.Core.DTOs.Company;
using XpertAcademy.Core.DTOs.Instractor;
using XpertAcademy.Core.DTOs.Pagination;
using XpertAcademy.Core.Services;
using XpertAcademy.Service.Services;

namespace XpertAcademy.APIs.Controllers
{

    public class CompaniesController : ApiBaseController
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost("AddTrustedCompany")]
        public async Task<ActionResult<CompanyToReturnDto>> AddTrustedCompany([FromForm] CreateCompanyDto dto)
        {
            try
            {
                var company = await _companyService.AddNewCompanyAsync(dto);

                return Ok(company);
            }
            catch (Exception ex)
            {

                return BadRequest(new { Message = $"{ex.Message}" });
            }
        }

        [HttpGet("GetAllTrustedCompanies")]
        public async Task<ActionResult<Pagination<IReadOnlyList<CompanyToReturnDto>>>> GetAllCompanies([FromQuery] PaginationDto dto)
        {
            try
            {
                var companies = await _companyService.GetAllCompaniesAsync(dto);

                int count = await _companyService.GetAllCompaniesCount();

                return Ok(new Pagination<CompanyToReturnDto>(dto.PageIndex, dto.PageSize, companies, count));
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = $"{ex.Message}" });
            }
        }

        [HttpGet("GetTrustedCompanyDetails/{companyId}")]
        public async Task<ActionResult<CompanyToReturnDto>> GetTrustedCompanyDetails(int companyId)
        {
            try
            {
                var company = await _companyService.GetCompanyDetailsAsync(companyId);

                return Ok(company);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = $"{ex.Message}" });
            }
        }

        [HttpPut("EditTrustedCompany/{companyId}")]
        public async Task<ActionResult<CompanyToReturnDto>> EditCompany(int companyId, [FromForm] CreateCompanyDto dto)
        {
            try
            {
                var company = await _companyService.UpdateCompanyAsync(companyId, dto);

                return Ok(company);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = $"{ex.Message}" });
            }
        }

        [HttpDelete("DeleteCompany/{companyId}")]
        public async Task<ActionResult> DeleteCompany(int companyId)
        {
            try
            {
                var result = await _companyService.DeleteCompanyAsync(companyId);

                return Ok(new { message = "Company deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"{ex.Message}" });
            }
        }
    }
}
