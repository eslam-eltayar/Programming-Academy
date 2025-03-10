using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XpertAcademy.Core.Models;

namespace XpertAcademy.Reposatories._Identity
{
    public static class ApplicationIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    Email = "adminamrawad@admin.com",
                    UserName = "amrawad",      
                    
                };

                await userManager.CreateAsync(user, "P@ssw0rd");
            }
        }
    }
}
