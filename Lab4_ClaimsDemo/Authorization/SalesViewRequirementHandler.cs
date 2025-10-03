using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using Lab4_ClaimsDemo.Models;

namespace Lab4_ClaimsDemo.Authorization
{
    public class SalesViewRequirementHandler : AuthorizationHandler<SalesViewRequirement, Product>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            SalesViewRequirement requirement,
            Product resource)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Task.CompletedTask;

            // Admin xem được tất cả
            if (context.User.HasClaim(c => c.Type == "Admin"))
            {
                context.Succeed(requirement);
            }
            // Sales chỉ xem sản phẩm do mình tạo
            else if (context.User.HasClaim(c => c.Type == "Sales") && resource.CreatedBy == userId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
