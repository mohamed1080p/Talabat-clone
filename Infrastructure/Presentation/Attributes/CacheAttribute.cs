using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    internal class CacheAttribute(int DurationInSeconds=90):ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // create cache key
            string CachKey = CreateCacheKey(context.HttpContext.Request);


            // search for value with cache key
            ICacheService cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var CacheValue = await cacheService.GetAsync(CachKey);


            // return value if it is not null
            if (CacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = CacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }


            // invoke .Next
            var ExecutedContext = await next.Invoke();


            // set value woth cache key
            if(ExecutedContext.Result is OkObjectResult result)
            {
                await cacheService.SetAsync(CachKey, result.Value, TimeSpan.FromSeconds(DurationInSeconds));
            }


        }

        private string CreateCacheKey(HttpRequest request)
        {
            StringBuilder Key = new();
            Key.Append(request.Path + '?');
            foreach (var item in request.Query.OrderBy(a=>a.Key))
            {
                Key.Append($"{item.Key}={item.Value}&");
            }
            return Key.ToString();
        }
    }
}
