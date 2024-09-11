using System.Text.Json;
using Talabat.APIs.Errors;

namespace Talabat.APIs.MiddleWares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _NextMidWare;
        private readonly ILogger<ExceptionMiddleWare> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleWare(RequestDelegate NextMidWare , ILogger<ExceptionMiddleWare> logger , IHostEnvironment env)
        {
            _NextMidWare = NextMidWare;
            this.logger = logger;
            this.env = env;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _NextMidWare.Invoke(httpContext);
            }catch (Exception ex)
            {
                logger.LogError(ex , ex.Message);

                httpContext.Response.StatusCode = 500;
                httpContext.Response.ContentType = "application/json";
                var Response = env.IsDevelopment() ? new ApiExciptionError(500, ex.Message, ex.StackTrace.ToString())
                                                   : new ApiExciptionError(500, ex.Message);

                var JsonOptions = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var JsonRespoine = JsonSerializer.Serialize(Response , JsonOptions);
               await httpContext.Response.WriteAsync(JsonRespoine);
            }
        }
    }
}
