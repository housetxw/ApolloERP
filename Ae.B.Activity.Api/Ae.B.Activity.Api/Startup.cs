﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Reflection;
using ApolloErp.Web.Swagger;
using System.Net.Http;
using log4net.Repository;
using ApolloErp.Log;
using Ae.B.Activity.Api.Extension;
using ApolloErp.Login.Auth;
using Ae.B.Activity.Api.Core.Interfaces;
using Ae.B.Activity.Api.Imp.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ae.B.Activity.Api.Client.Clients;
using Ae.B.Activity.Api.Filters;
using ApolloErp.Component.Http;
using Ae.B.Activity.Api.Common.Format;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

namespace Ae.B.Activity.Api
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


            //日志链路增加CorrelationId
            services.AddApolloErpCorrelationId();
            //请求验证fiter自动注入，与上面的AddApolloErpCorrelationId必须一起使用
            services.AddMvcCore(option =>
            {
                option.Filters.Add(new FilterCorrelationIdAttribute(string.Empty));
            });

            //添加DB组件
            services.AddDapper(Configuration)
                .AddConnectionString("ActivitySql")
                .AddConnectionString("ActivitySqlReadOnly");

            services.AddNlog();
            services.AddTransient<PromoteClient, PromoteClient>();
            services.AddTransient<InstallGuideClient, InstallGuideClient>();            
            services.AddClient(Configuration);
            //增加登录认证注册
            services.AddLoginAuth(Configuration);

            //指定需要注入程序集数组 
            services.RegisterService(new string[] {
                "Ae.B.Activity.Api.Imp","Ae.B.Activity.Api.Client"
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
                app.UseSwagger(AssemblyName, Configuration);
                app.UseHsts();
            };
            app.UseApolloErpCorrelationId();

            //单体方法开启登录认证使用
            //app.UseAuthentication();

            //所有调用开启登录认证使用
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
