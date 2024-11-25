using DatingApp.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace DatingApp.Helpers
{
    public class LogActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var ResultContxt = await next();

            var userId=ResultContxt.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var Repo = ResultContxt.HttpContext.RequestServices.GetService<IRepository>();

            var CurrantUser = await Repo.GetUserbyIdAsync(int.Parse(userId));

            CurrantUser.LastActive = DateTime.UtcNow;

            await Repo.SaveAllAsync();
        }
    }
}
