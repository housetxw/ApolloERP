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
using Ae.C.Login.Api.Core.Interfaces;
using Ae.C.Login.Api.Imp.Services;
using Ae.C.Login.Api.Dal.Repositorys.User;
using Ae.C.Login.Api.Dal.Repositorys.UserThird;
using Ae.C.Login.Api.Client.Clients.WeChatService;
using Ae.C.Login.Api.Filters;
using log4net.Repository;
using log4net;
using log4net.Config;
using System.IO;
using NLog.Web;
using NLog.Config;
using NLog.Extensions.Logging;
using ApolloErp.Log;
using Ae.C.Login.Api.Core.Model;
using Ae.C.Login.Api.Extension;
using ApolloErp.Redis;
using Ae.C.Login.Api.Common.Constant;
using Ae.C.Login.Api.Common.Extension;
using ApolloErp.Message.Sms;
using ApolloErp.Component.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Ae.C.Login.Api.Common.Format;

namespace Ae.C.Login.Api
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
            //services.AddControllers()
            //.AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter("yyyy-MM-dd HH:mm:ss")); });
            services.AddMvc(options => {
                options.EnableEndpointRouting = false;  //关闭Endpoint的路由支持来兼容
                options.SuppressAsyncSuffixInActionNames = false;  //关闭新特性：Async结尾会默认去除
            })
                .AddNewtonsoftJson()
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
                .AddConnectionString("UserSql")
                .AddConnectionString("UserSqlReadOnly");

            services.AddMemoryCache();

            services.AddClient(Configuration);
            services.AddNlog();
            services.AddSms();
            services.AddRedis();

            //日志链路增加CorrelationId
            services.AddApolloErpCorrelationId();
            //请求验证fiter自动注入，与上面的AddApolloErpCorrelationId必须一起使用
            services.AddMvcCore(option =>
            {
                option.Filters.Add(new FilterCorrelationIdAttribute(string.Empty));
            });


            //指定需要注入程序集数组 
            services.RegisterService(new string[] {
                "Ae.C.Login.Api.Client" ,
                "Ae.C.Login.Api.Imp",
                "Ae.C.Login.Api.Dal",
            });
            //services.TryAddScoped<ILoginAuthService, LoginAuthService>();
            //services.TryAddScoped<ILoginAuthService, LoginAuthService>();

            #region 读取配置信息
            //jwt
            services.Configure<JwtConfig>(Configuration.GetSection("JWT"));
            JwtConfig config = new JwtConfig();
            Configuration.GetSection("JWT").Bind(config);
            #endregion

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseRouting();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Swagger
                //var swaggerOnProd = Configuration["EnableSwagger"].EqualsIgnoreCase(CommonConstant.TrueStr);
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

            var swaggerOnProd = Configuration["EnableSwagger"].EqualsIgnoreCase(CommonConstant.TrueStr);
            if (swaggerOnProd)
            {
                app.UseSwagger(AssemblyName, Configuration);
            }
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
