using Store.Service.HandleResponse;
using System.Net;
using System.Text.Json;

namespace Store.Web.MiddleWare
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate Next , ILogger<ExceptionMiddleware> logger , IHostEnvironment env )
        {
            next = Next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }catch (Exception ex)
            {
                logger.LogError(ex,ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var ResponseEnv = env.IsDevelopment() ? new CustomException(500, ex.Message, ex.StackTrace.ToString())
                    : new CustomException((int)HttpStatusCode.InternalServerError);
                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var JsonResponse = JsonSerializer.Serialize(ResponseEnv, options);
                await context.Response.WriteAsync(JsonResponse);
            }
        }
    }
}
