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
using Ae.Receive.Service.Core.Interfaces;
using Ae.Receive.Service.Imp.Services;
using Ae.Receive.Service.Dal.Repositorys.Test;
using Ae.Receive.Service.Client.Clients.ShopServer;
using Ae.Receive.Service.Filters;
using log4net.Repository;
using log4net;
using log4net.Config;
using System.IO;
using NLog.Web;
using NLog.Config;
using NLog.Extensions.Logging;
using ApolloErp.Log;
using Ae.Receive.Service.Client.Clients;
using Ae.Receive.Service.Dal.Repositorys;
using Ae.Receive.Service.Client.Inteface;
using Ae.Receive.Service.Extension;
using ApolloErp.Component.Http;

namespace Ae.Receive.Service
{
    public class Startup
    {
        private static AssemblyName AssemblyName => typeof(Startup).Assembly.GetName();
        public static ILoggerRepository repository { get; set; }
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //repository = LogManager.CreateRepository("NetCoreApp"); 
            //XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            //LogHelper.Configure(); //使用前先配置
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //配置跨域处理
            services.AddCors(options =>
            {
                options.AddPolicy("AllowCors", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
            });

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Swagger
            services.AddSwagger(AssemblyName, Configuration, c =>
            {
                c.CustomSchemaIds(x => x.FullName);
            });

            //添加DB组件
            services.AddDapper(Configuration)
                .AddConnectionString("ReceiveSql")
                .AddConnectionString("ReceiveSqlReadOnly");

            services.AddNlog();

            services.AddClient(Configuration);
 
            //指定需要注入程序集数组 
            services.RegisterService(new string[] {
                "Ae.Receive.Service.Client" ,
                "Ae.Receive.Service.Imp",
                "Ae.Receive.Service.Dal",
            });

            //日志链路增加CorrelationId
            services.AddApolloErpCorrelationId();
            //请求验证fiter自动注入，与上面的AddApolloErpCorrelationId必须一起使用
            services.AddMvcCore(option =>
            {
                option.Filters.Add(new FilterCorrelationIdAttribute(string.Empty));
            });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (!env.IsProduction())
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
                // Swagger
                app.UseSwagger(AssemblyName, Configuration, dev: true);
                app.UseHsts();
            };
            //开启CorrelationId中间件
            app.UseApolloErpCorrelationId();

            app.UseHttpsRedirection();
            app.UseCors("AllowCors");
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}");
            });


        }
    }
}
