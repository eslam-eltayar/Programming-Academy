using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XpertAcademy.APIs.Helpers;
using XpertAcademy.Core.DTOs.ContactUs;
using XpertAcademy.Core.DTOs.Pagination;
using XpertAcademy.Core.Services;
using XpertAcademy.Service.Services;

namespace XpertAcademy.APIs.Controllers
{

    public class ContactUsController : ApiBaseController
    {
        private readonly IContactUsService _contactUsService;

        public ContactUsController(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }

        [HttpPost("AddContactUs")]
        public async Task<ActionResult<ContactUsDto>> AddContactUs([FromBody] CreateContactUsDto dto)
        {
            try
            {
                var contact = await _contactUsService.AddNewAddContactUs(dto);

                return Ok(contact);
            }
            catch (Exception ex)
            {

                return BadRequest(new { Message = $"{ex.Message}" });
            }
        }

        [HttpGet("GetAllContactUs")]
        public async Task<ActionResult<Pagination<IReadOnlyList<ContactUsDto>>>> GetAllContactUs([FromQuery] PaginationDto dto)
        {
            try
            {
                var contactUs = await _contactUsService.GetAllContactUsAsync(dto);

                int count = await _contactUsService.GetAllContactUsCount();

                return Ok(new Pagination<ContactUsDto>(dto.PageIndex, dto.PageSize, contactUs, count));
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = $"{ex.Message}" });
            }
        }

        [HttpGet("GetContactUsDetails/{Id}")]
        public async Task<ActionResult<ContactUsDto>> GetContactUsDetails(int Id)
        {
            try
            {
                var contactUs = await _contactUsService.GetContactUsDetailsAsync(Id);

                return Ok(contactUs);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = $"{ex.Message}" });
            }
        }

        [HttpDelete("DeleteContactUs/{Id}")]
        public async Task<ActionResult> DeleteContactUs(int Id)
        {
            try
            {
                var result = await _contactUsService.DeleteContactUs(Id);

                return Ok(new { Message = "Contact Us Deleted Successfuly" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"{ex.Message}" });
            }
        }
    }
}
