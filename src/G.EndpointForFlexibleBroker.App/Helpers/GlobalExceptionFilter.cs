using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace G.EndpointForFlexibleBroker.App.Helpers
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogCritical(context.Exception, context.Exception.Message);
            string message = "Unexpected server error. Please contact with our help support.";
            HttpStatusCode status = HttpStatusCode.InternalServerError;

            context.ExceptionHandled = true;

            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "text/plain";
            var err = message;
            response.WriteAsync(err);
        }
    }
}
