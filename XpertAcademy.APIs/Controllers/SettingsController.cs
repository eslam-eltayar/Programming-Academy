using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XpertAcademy.Core.Models;
using XpertAcademy.Core.Services;

namespace XpertAcademy.APIs.Controllers
{
    public class SettingsController : ApiBaseController
    {
        private readonly ISettingsService _settingsService;

        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }


        [HttpGet("GetSettings")]
        public async Task<ActionResult<Settings>> GetSettings()
        {
            try
            {
                var settings = await _settingsService.GetSettingsAsync();

                return Ok(settings);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = $"{ex.Message}" });
            }
        }


        [HttpPut("UpdateSettings")]
        public async Task<ActionResult<Settings>> AddSettings([FromBody] Settings settings)
        {
            try
            {
                var setting = await _settingsService.UpdateSettingsAsync(settings);

                return Ok(settings);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = $"{ex.Message}" });
            }
        }
    }
}
