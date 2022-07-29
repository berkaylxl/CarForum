using CarForum.Common.Exceptions;
using CarForum.Common.Infrastructure.Results;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace CarForum.Api.WebApi.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder ConfigureExceptionHandling(this IApplicationBuilder app,
                                                                     bool inclueExceptionDetails = false,
                                                                     bool useDefaultHandlingResponse = true,
                                                                     Func<HttpContext, Exception, Task> handleException = null)
        {
            app.Run(context =>
            {
                var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
                if (!useDefaultHandlingResponse && handleException == null)
                    throw new ArgumentNullException(nameof(handleException),
                       $"{nameof(handleException)} cannot be null when {nameof(useDefaultHandlingResponse)} is false");

                if (!useDefaultHandlingResponse && handleException != null)
                    return handleException(context, exceptionObject.Error);

                return DefaultHandleException(context, exceptionObject.Error, inclueExceptionDetails);
            });
            return app;
        }
        private static async Task DefaultHandleException(HttpContext context, Exception exception, bool includeException)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string message = "Internal server error occured";
            if (exception is UnauthorizedAccessException)
                statusCode = HttpStatusCode.Unauthorized;

            if (exception is DatabaseValidationExcepton)
            {
                var validationResponse = new ValidationResponseModel(exception.Message);
                await WriteReponse(context, statusCode, validationResponse);
                return;
            }
            var res = new
            {
                HttpStatusCode = (int)statusCode,
                Detail = includeException ? exception.ToString() : message
            };
            await WriteReponse(context, statusCode, res);

        }
        private static async Task WriteReponse(HttpContext context, HttpStatusCode httpStatusCode, object responseObj)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;

            await context.Response.WriteAsJsonAsync(responseObj);
        }

    }
}
