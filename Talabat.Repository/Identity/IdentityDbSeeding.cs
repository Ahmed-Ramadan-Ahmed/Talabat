using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public static class IdentityDbSeeding
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "ADMIN Ahmed Ramadan",
                    Email = "Ara@gmail.com",
                    UserName = "AhmedRamadan",
                    PhoneNumber = "01552372197"
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
