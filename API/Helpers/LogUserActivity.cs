using System.Threading.Tasks;
using API.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using API.Repository;
using API.Interfaces;

namespace API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            var userId = resultContext.HttpContext.User.GetUserId();
            var ouw = resultContext.HttpContext.RequestServices.GetService<IUnitOfWork>();
            var user = await ouw.UserRepository.getUserByIdAsync(userId);
            user.LastActive = DateTime.Now;
            await ouw.Complete();
        }
    }
}