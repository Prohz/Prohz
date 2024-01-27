using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace KopkeHome_UtilityLayer.ExceptionHandler
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {

                await _next.Invoke(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                HandleExceptionMessageAsync(context, ex);
                _logger.LogError(ex.Message);
                context.Response.Redirect("/Home/error");

            }
        }
        private static void HandleExceptionMessageAsync(HttpContext context, Exception exception)
        {
            //  LogManager.WriteErrorLog(exception);
        }
    }
}
