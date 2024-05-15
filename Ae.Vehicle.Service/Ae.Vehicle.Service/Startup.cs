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
using Ae.Vehicle.Service.Dal.Repository;
using Ae.Vehicle.Service.Core.Interfaces;
using Ae.Vehicle.Service.Imp.Services;
using Ae.Vehicle.Service.Validators;
using ApolloErp.Redis;
using Ae.Vehicle.Service.Filters;
using Ae.Vehicle.Service.Common.Format;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

namespace Ae.Vehicle.Service
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
            //        o.RegisterValidatorsFromAssemblyContaining<GetVehicleListByBrandRequestValidator>();
            //        o.RegisterValidatorsFromAssemblyContaining<PaiLiangByVehicleIdRequestValidator>();
            //        o.RegisterValidatorsFromAssemblyContaining<VehicleNianByPaiLiangRequestValidator>();
            //        o.RegisterValidatorsFromAssemblyContaining<VehicleSalesNameByNianRequestValidator>();
            //    });
            services.AddControllers()
            .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter("yyyy-MM-dd HH:mm:ss")); });

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
                .AddConnectionString("ProductReadOnly")
                .AddConnectionString("User")
                .AddConnectionString("UserReadOnly");

            services.AddHttpClient("ApolloErp", "www.ApolloErp.cn");
            services.AddNlog();
            services.AddRedis();
            //RGBuilderExtensions.AddServices(services);
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IVehicleOperateService, VehicleOperateService>();
            services.AddScoped<IVehicleBrandRepository, VehicleBrandRepository>();
            services.AddScoped<IVehicleTypeRepository, VehicleTypeRepository>();
            services.AddScoped<IVehicleTypeTimingRepository, VehicleTypeTimingRepository>();
            services.AddScoped<IVehicleTypeTimingConfigRepository, VehicleTypeTimingConfigRepository>();
            services.AddScoped<IVehicleTypeTimingTiresMatchRepository, VehicleTypeTimingTiresMatchRepository>();
            services.AddScoped<IUserCarRepository, UserCarRepository>();
            services.AddScoped<IUserCarPropertyRepository, UserCarPropertyRepository>();
            services.AddScoped<IVehicleTypeTimingLiyangRepository, VehicleTypeTimingLiyangRepository>();
            services.AddScoped<IVehicleTypeTimingIdMapRepository, VehicleTypeTimingIdMapRepository>();
            services.AddScoped<IUserCarComponentsRepository, UserCarComponentsRepository>();
            services.AddScoped<IUserCarComponentsResultRepository, UserCarComponentsResultRepository>();
            services.AddScoped<IVehicleTypeTimingCopyRepository, VehicleTypeTimingCopyRepository>();
            services.AddScoped<IBaoYangPartsRepository, BaoYangPartsRepository>();
            services.AddScoped<IBaoYangPartAccessoryRepository, BaoYangPartAccessoryRepository>();
            services.AddScoped<IBaoYangPartsLiyangRepository, BaoYangPartsLiyangRepository>();
            services.AddScoped<IBaoYangPartAccessoryLiyangRepository, BaoYangPartAccessoryLiyangRepository>();

            //日志链路增加CorrelationId
            services.AddApolloErpCorrelationId();
            //请求验证fiter自动注入，与上面的AddApolloErpCorrelationId必须一起使用
            services.AddMvcCore(option =>
            {
                option.Filters.Add(new FilterCorrelationIdAttribute(string.Empty));
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
                app.UseSwagger(AssemblyName, Configuration);
                app.UseHsts();
            };

            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            //});

            //开启CorrelationId中间件
            app.UseApolloErpCorrelationId();

            app.UseHttpsRedirection();
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
