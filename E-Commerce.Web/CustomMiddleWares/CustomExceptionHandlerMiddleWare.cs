using Domain.Exceptions;
using Shared.ErrorModels;
using System.Text.Json;

namespace E_Commerce.Web.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate Next, ILogger<CustomExceptionHandlerMiddleWare> logger)
        {
            _next = Next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);

                await HandleNotFoundEndPointAsync(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");

                // set status code for response

                await HandleExceptionAsync(context, ex);

            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var Response = new ErrorToReturn()
            {
                ErrorMessage = ex.Message
            };
            var ResponseCode = context.Response.StatusCode = ex switch
            {
                BadRequestException badRequestException=>GetBadRequestErrors(badRequestException, Response),
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException=>StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError
            };


            // set content type for response
            //context.Response.ContentType = "application/json";

            // response object
           


            //return object as JSON
            await context.Response.WriteAsJsonAsync(Response);
        }

        private static int GetBadRequestErrors(BadRequestException badRequestException, ErrorToReturn response)
        {
            response.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext context)
        {
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End point {context.Request.Path} is not found"
                };
                await context.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}
