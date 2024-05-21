using AutoMapper;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ae.Shop.Api.Extension;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.Swagger;
using System;
using System.Reflection;
using ApolloErp.Component.Http;
using Ae.Shop.Api.Filters;
using Ae.Shop.Api.Common.Format;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

namespace Ae.Shop.Api
{
    public class Startup
    {
        private static AssemblyName AssemblyName => typeof(Startup).Assembly.GetName();
        public static ILoggerRepository repository { get; set; }
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowCors", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                    //.AllowCredentials();
                });
            });

            //services.AddMvc()
            //    .AddJsonOptions(options =>
            //    {
            //        options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            //    })
            //    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //services.AddControllers()
            //.AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter("yyyy-MM-dd HH:mm:ss")); });
            services.AddMvc(options => {
                options.EnableEndpointRouting = false;  //关闭Endpoint的路由支持来兼容
                options.SuppressAsyncSuffixInActionNames = false;  //关闭新特性：Async结尾会默认去除
            })
                .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter("yyyy-MM-dd HH:mm:ss")); });

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Swagger
            services.AddSwagger(AssemblyName, Configuration, c =>
            {
                c.CustomSchemaIds(x => x.FullName);
            });
            //日志链路增加CorrelationId
            services.AddApolloErpCorrelationId();
            //请求验证fiter自动注入，与上面的AddApolloErpCorrelationId必须一起使用
            services.AddMvcCore(option =>
            {
                option.Filters.Add(new FilterCorrelationIdAttribute(string.Empty));
            });
            //ConnectionString
            services.AddDapper(Configuration)
                .AddConnectionString("ApolloErpSql")
                .AddConnectionString("ApolloErpSqlReadOnly")
                .AddConnectionString("ShopManageSql")
                .AddConnectionString("ShopManageSqlReadOnly")
                .AddConnectionString("ShopWMSSql")
                .AddConnectionString("ShopWMSSqlReadOnly")
                .AddConnectionString("ProductSql")
                .AddConnectionString("ProductSqlReadOnly")
                .AddConnectionString("ReceiveSql")
                .AddConnectionString("ReceiveSqlReadOnly")
                .AddConnectionString("ShopPurchaseSql")
                .AddConnectionString("ShopPurchaseSqlReadOnly")
                .AddConnectionString("ShopOrderSql")
                .AddConnectionString("ShopOrderSqlReadOnly")
                 .AddConnectionString("OrderSql")
                .AddConnectionString("OrderSqlReadOnly")
                  .AddConnectionString("ShopStockSql")
                .AddConnectionString("ShopStockSqlReadOnly")
             .AddConnectionString("WMSSql")
                .AddConnectionString("WMSSqlReadOnly");
            services.AddNlog();

            services.AddClient(Configuration);

            services.AddLoginAuth(Configuration);

            services.RegisterService(new string[] {
                "Ae.Shop.Api.Client" ,
                "Ae.Shop.Api.Imp",
                "Ae.Shop.Api.Dal",
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseRouting();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Swagger
#if DEBUG
                app.UseSwagger(AssemblyName, Configuration, dev: true);
#endif
#if !DEBUG
                app.UseSwagger(AssemblyName, Configuration);
#endif
            }
            else
            {
                app.UseHsts();
            };

            app.UseSwagger(AssemblyName, Configuration);
            app.UseApolloErpCorrelationId();

            app.UseLoginAuth(Configuration);

            app.UseHttpsRedirection();
            app.UseCors("AllowCors");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}");
            });
            //app.UseEndpoints(endpoints =>
            //{
            //    // 设置默认路由
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello, World!");
            //    });

            //    // 配置控制器路由
            //    endpoints.MapControllers();
            //});
        }
    }
}
