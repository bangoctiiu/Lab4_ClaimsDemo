using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System.Threading.Tasks;
using Lab4_ClaimsDemo.Models;
using System.Linq; // Cần để dùng Any()

namespace Lab4_ClaimsDemo.Data
{
    public static class SeedData
    {
        public static async Task EnsureSeededAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            // Lấy DbContext để thao tác với bảng Products
            var context = services.GetRequiredService<ApplicationDbContext>();

            // ------------------- Seed Users (Logic của bạn đã tốt) -------------------
            // Tạo admin
            var adminEmail = "admin@gmail.com";
            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(admin, "Admin123@");
                if (result.Succeeded)
                {
                    await userManager.AddClaimAsync(admin, new Claim("CreateProduct", "true"));
                    await userManager.AddClaimAsync(admin, new Claim("Admin", "true"));
                }
            }

            // Tạo sales
            var salesEmail = "sales@gmail.com";
            var sales = await userManager.FindByEmailAsync(salesEmail);
            if (sales == null)
            {
                sales = new ApplicationUser
                {
                    UserName = salesEmail,
                    Email = salesEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(sales, "Sale123@");
                if (result.Succeeded)
                {
                    await userManager.AddClaimAsync(admin, new Claim("CreateProduct", "true"));
                    await userManager.AddClaimAsync(sales, new Claim("Sales", "true"));
                }
            }

            
        }
    }
}