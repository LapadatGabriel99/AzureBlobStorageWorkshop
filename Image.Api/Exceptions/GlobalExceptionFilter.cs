using Image.Api.Dto.Common;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Image.Api.Exceptions
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var response = new ErrorResponse();
            response.Errors.Add("Something went wrong!");
            response.Errors.Add($"Exception: {context.Exception.Message}");

            if (context.Exception.InnerException != null)
            {
                response.Errors.Add($"InnerException: {context.Exception.Message}");
            }

            response.Errors.Add($"StackTrace: {context.Exception.StackTrace}");

            context.Result = new InternalServerErrorObjectResult(response);
            context.ExceptionHandled = true;
        }
    }
}
