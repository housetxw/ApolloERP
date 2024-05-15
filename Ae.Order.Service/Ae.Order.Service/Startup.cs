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
using Ae.Order.Service.Core.Interfaces;
using Ae.Order.Service.Imp.Services;
using Ae.Order.Service.Dal.Repository.Order;
using ApolloErp.Log;
using Ae.Order.Service.Extension;
using Ae.Order.Service.Filters;
using ApolloErp.Component.Http;
using Ae.Order.Service.Common.Format;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

namespace Ae.Order.Service
{
    public class Startup
    {
        private static AssemblyName AssemblyName => typeof(Startup).Assembly.GetName();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
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

            // Swagger
            services.AddSwagger(AssemblyName, Configuration, c =>
            {
                c.CustomSchemaIds(x => x.FullName);
            });

            //添加DB组件
            services.AddDapper(Configuration)
                .AddConnectionString("OrderSql")
                .AddConnectionString("OrderSqlReadOnly");

            services.AddNlog();
            services.AddClient(Configuration);
            //日志链路增加CorrelationId
            services.AddApolloErpCorrelationId();
            //请求验证fiter自动注入，与上面的AddApolloErpCorrelationId必须一起使用
            services.AddMvcCore(option =>
            {
                option.Filters.Add(new FilterCorrelationIdAttribute(string.Empty));
            });

            //指定需要注入程序集数组 
            services.RegisterService(new string[] {
                "Ae.Order.Service.Client" ,
                "Ae.Order.Service.Imp",
                "Ae.Order.Service.Dal"
            });
        }

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
