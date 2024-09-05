namespace Books.API.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<RequestResponseLoggingMiddleware> logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            logger.LogInformation($"Incoming Request: {context.Request.Method} {context.Request.Path}");

            await next(context);

            logger.LogInformation($"Outgoing Request: {context.Response.StatusCode}");
        }
    }
}
