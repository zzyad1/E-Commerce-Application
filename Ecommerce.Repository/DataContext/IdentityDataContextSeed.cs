using E_Commerce.core.Entities;
using E_Commerce.core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace Ecommerce.Repository.DataContext
{
    public static class IdentityDataContextSeed 
    {
        public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    UserName ="ZyadMostafa",
                    Email = "zyad.mostafa555@gmail.com",
                    DisplayName ="zyad mostafa",
                    Address = new Address()
                    {
                        City ="Giza",
                        Country ="Egypt",
                        PostalCode= "12-456-32",
                        State = "ElShikh-Zayed",
                        Street ="37",
                    }
                }; 

                await userManager.CreateAsync(user , "Password123456@");
                
            }

        }
 
    }
}
