using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;


namespace Ae.C.Login.Api.Extension
{
    namespace Ae.C.Login.Api
    {
        public static class AppBuilderExtensions
        {
            public static void UseDefaultExceptionHandler(this IApplicationBuilder app, IHostingEnvironment env)
            {
                app.UseExceptionHandler(new DefaultExceptionHandlerOptions(env));
            }

            public static void UseDefaultStatusCodePages(this IApplicationBuilder app, IHostingEnvironment env)
            {
                app.UseStatusCodePages(async context =>
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    await context.HttpContext.Response.WriteAsync(string.Format("{{\"errCode\": 0,\"errMsg\": null,\"subErrCode\": {0},\"subErrMsg\": \"接口URL路径无效\"}}",
                        context.HttpContext.Response.StatusCode));
                });
            }
        }
    }
}
