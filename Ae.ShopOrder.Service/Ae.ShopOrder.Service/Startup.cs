using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AutoMapper;
using Swashbuckle.AspNetCore.Swagger;
using ApolloErp.Web.WebApi;
using System.Reflection;
using ApolloErp.Web.Swagger;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ae.ShopOrder.Service.Core.Interfaces;
using Ae.ShopOrder.Service.Imp.Services;
using Ae.ShopOrder.Service.Dal.Repositorys.Test;
using Ae.ShopOrder.Service.Client.Clients.ShopServer;
using Ae.ShopOrder.Service.WebApi.Filters;
using log4net.Repository;
using log4net;
using log4net.Config;
using System.IO;
using NLog.Web;
using NLog.Config;
using NLog.Extensions.Logging;
using ApolloErp.Log;
using Ae.ShopOrder.Service.Dal.Repository;
using Ae.ShopOrder.Service.Extension;
using ApolloErp.Component.Http;
using Ae.ShopOrder.Service.Filters;
using Ae.ShopOrder.Service.Common.Format;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

namespace Ae.ShopOrder.Service.WebApi
{
    /// <summary>
    /// 项目开始
    /// </summary>
    public class Startup
    {
        private static AssemblyName AssemblyName => typeof(Startup).Assembly.GetName();
        public static ILoggerRepository repository { get; set; }
        public IConfiguration Configuration { get; }
        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //repository = LogManager.CreateRepository("NetCoreApp"); 
            //XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            //LogHelper.Configure(); //使用前先配置
        }

        /// <summary>
        /// 注入对象
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //配置跨域处理
            services.AddCors(options =>
            {
                options.AddPolicy("AllowCors", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                    //.AllowCredentials();//指定处理cookie
                });
            });

            //services.AddMvc()
            //    .AddJsonOptions(options =>
            //    {
            //        options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            //    })
            //    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddControllers()
            .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter("yyyy-MM-dd HH:mm:ss")); });

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState
                        .Values
                        .SelectMany(x => x.Errors
                            .Select(p => p.ErrorMessage))
                        .ToList();
                    ApiResult result = new ApiResult()
                    {
                        Code = ResultCode.Failed,
                        Message = errors.LastOrDefault()
                    };

                    return new BadRequestObjectResult(result);
                };
            });

            // Swagger
            services.AddSwagger(AssemblyName, Configuration, c =>
            {
                c.CustomSchemaIds(x => x.FullName);
            });

            services.AddNlog();

            services.AddHttpClient();

            services.AddClient(Configuration);

            //添加DB组件
            services.AddDapper(Configuration)
                .AddConnectionString("ShopOrderSql")
                .AddConnectionString("ShopOrderSqlReadOnly")
                .AddConnectionString("OrderSql")
                .AddConnectionString("OrderSqlReadOnly")
                 .AddConnectionString("OrderConfigSql")
                .AddConnectionString("OrderConfigSqlReadOnly");

            //指定需要注入程序集数组 
            services.RegisterService(new string[] {
                "Ae.ShopOrder.Service.Client" ,
                "Ae.ShopOrder.Service.Imp",
                "Ae.ShopOrder.Service.Dal"
            });
            //日志链路增加CorrelationId
            services.AddApolloErpCorrelationId();
            //请求验证fiter自动注入，与上面的AddApolloErpCorrelationId必须一起使用
            services.AddMvcCore(option =>
            {
                option.Filters.Add(new FilterCorrelationIdAttribute(string.Empty));
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
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
                app.UseSwagger(AssemblyName, Configuration);
            };


            app.UseHttpsRedirection();
            //开启CorrelationId中间件
            app.UseApolloErpCorrelationId();
            app.UseCors("AllowCors");
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        "default",
            //        "{controller=Home}/{action=Index}");
            //});
            app.UseEndpoints(endpoints =>
            {
                // 设置默认路由
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello, World!");
                });

                // 配置控制器路由
                endpoints.MapControllers();
            });

        }
    }
}
