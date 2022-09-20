using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace SportSrore.Models
{
    public static class IdentitySeedData
    {
        private const string _adminUser = "Admin";
        private const string _adminPassword = "Secret123$";

        public static async void EnsurePopulated(/*UserManager<IdentityUser> userManager*/ IApplicationBuilder app)
        {
            
            UserManager<IdentityUser> userManager = app.ApplicationServices
                .GetRequiredService<UserManager<IdentityUser>>();
            
            IdentityUser user = await userManager.FindByIdAsync(_adminUser);

            if(user == null)
            {
                user = new IdentityUser("Admin");
                await userManager.CreateAsync(user, _adminPassword);
            }
        }
    }
}
