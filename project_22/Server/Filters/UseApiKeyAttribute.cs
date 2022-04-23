using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace project_22.Server.Filters
{
    public class UseApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = configuration.GetValue<string>("ApiKeys:ApiKey");

            if (!context.HttpContext.Request.Query.TryGetValue("key", out var key))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!apiKey.Equals(key))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
