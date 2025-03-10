using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XpertAcademy.Core.DTOs.Identity;
using XpertAcademy.Core.Models;
using XpertAcademy.Core.Services.Identity;

namespace XpertAcademy.APIs.Controllers
{
    public class AccountController : ApiBaseController
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == model.Email);

            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid Login. This Email doesn't Exist" });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
                return Unauthorized(new { Message = "Invalid Login." });

            return Ok(new UserDto()
            {
                Email = user?.Email ?? string.Empty,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });
        }
    }
}
