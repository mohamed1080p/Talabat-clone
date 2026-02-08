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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong");

                // set status code for response

                var ResponseCode = context.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };


                // set content type for response
                context.Response.ContentType = "application/json";

                // response object
                var Response = new ErrorToReturn()
                {
                    StatusCode = ResponseCode,
                    ErrorMessage = ex.Message
                };


                //return object as JSON
                await context.Response.WriteAsJsonAsync(Response);

            }
        }
    }
}
