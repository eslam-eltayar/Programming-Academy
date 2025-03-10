using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.Models;
using XpertAcademy.Core.Services.Identity;

namespace XpertAcademy.Service.Services.Identity
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
        {

            var authClaims = new List<Claim>()
            {

                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name , user.UserName)
            };

            /// if user has a Role or more .. 

            var userRoles = await userManager.GetRolesAsync(user);

            // add user's roles to Auth Claims

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:AuthKey"] ?? string.Empty));


            var token = new JwtSecurityToken(
                 audience: _configuration["JWT:ValidAudience"],
                 issuer: _configuration["JWT:ValidIssuer"],
                 expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"] ?? "0")),
                 claims: authClaims,
                 signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
                 );


            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
