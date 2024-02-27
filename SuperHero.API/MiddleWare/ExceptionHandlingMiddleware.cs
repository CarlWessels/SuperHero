using Newtonsoft.Json;
namespace SuperHero.API.MiddleWare

{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
#if DEBUG
                var result = JsonConvert.SerializeObject(new { error = ex.Message });
#else
                var result = "An error occurred while processing your request. Please contact support";
#endif
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }
        }
    }

}