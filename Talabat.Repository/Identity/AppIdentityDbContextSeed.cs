using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {

                var AppUser = new AppUser()
                {
                    DisplayName = "Mohamed Hussein",
                    Email = "Mohamed.h.yousuf@gmail.com",
                    UserName = "Mohamed.h.yousuf",
                    PhoneNumber = "01004826372"
                };
                await userManager.CreateAsync(AppUser, "Pa$$w0rd");
            }
        }
    }
}
