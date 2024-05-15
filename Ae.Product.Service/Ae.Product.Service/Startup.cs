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
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ApolloErp.Component.Http;
using ApolloErp.Log;
using ApolloErp.Redis;
using Ae.Product.Service.Dal.Repository;
using Ae.Product.Service.Imp.Services;
using Ae.Product.Service.Core.Interfaces;
using Ae.Product.Service.Validators;
using Ae.Product.Service.Core.Extension;
using Ae.Product.Service.Filters;
using Ae.Product.Service.Common.Format;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

namespace Ae.Product.Service
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
            //    .AddJsonOptions(options => { options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; })
            //    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddFluentValidation(o =>
            //    {
            //        o.RegisterValidatorsFromAssemblyContaining<AddBrandRequestValidator>();
            //    });
            services.AddControllers()
            .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter("yyyy-MM-dd HH:mm:ss")); });

            //配置跨域处理
            services.AddCors(options =>
            {
                options.AddPolicy("AllowCors", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                        //.AllowCredentials(); //指定处理cookie
                });
            });
            // override modelstate
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

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("v1", new Info { Title = "My WebApi Document", Version = "v1" });
            //});

            // Swagger
            services.AddSwagger(AssemblyName, Configuration, c => { c.CustomSchemaIds(x => x.FullName); });

            //添加DB组件
            services.AddDapper(Configuration)
                .AddConnectionString("Product")
                .AddConnectionString("ProductReadOnly");
            services.AddNlog();
            services.AddRedis();
            Configuration.AddClient(services);
            //指定需要注入程序集数组 
            services.RegisterService(new string[]
            {
                "Ae.Product.Service.Client",
                "Ae.Product.Service.Dal",
                "Ae.Product.Service.Imp"
            });

            //日志链路增加CorrelationId
            services.AddApolloErpCorrelationId();
            //请求验证fiter自动注入，与上面的AddApolloErpCorrelationId必须一起使用
            services.AddMvcCore(option => { option.Filters.Add(new FilterCorrelationIdAttribute(string.Empty)); });
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
                app.UseSwagger(AssemblyName, Configuration);
                app.UseHsts();
            }

            ;

            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            //});

            //开启CorrelationId中间件
            app.UseApolloErpCorrelationId();

            app.UseHttpsRedirection();
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
