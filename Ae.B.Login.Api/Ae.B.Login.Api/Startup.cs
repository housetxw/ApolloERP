using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Reflection;
using ApolloErp.Web.Swagger;
using log4net.Repository;
using Microsoft.AspNetCore.Http;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Message.Sms;
using ApolloErp.Redis;
using Ae.B.Login.Api.Common.Constant;
using Ae.B.Login.Api.Common.Extension;
using Ae.B.Login.Api.Core.Model;
using ApolloErp.Component.Http;
using Ae.B.Login.Api.Filters;
using Ae.B.Login.Api.Common.Format;
using Microsoft.Extensions.Hosting;

namespace Ae.B.Login.Api
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
            #region .Net Core configuration

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
        
            services.AddMvc(options => {
                options.EnableEndpointRouting = false;
                options.SuppressAsyncSuffixInActionNames = false;
            }).AddNewtonsoftJson()
            .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter("yyyy-MM-dd HH:mm:ss")); });

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #endregion .Net Core configuration

            #region Extended configuration

            services.AddNlog();

            //var temp = AppDomain.CurrentDomain.GetAssemblies();
            //injection AutoMapper component
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Swagger generation services to our container.
            services.AddSwagger(AssemblyName, Configuration, c =>
            {
                c.CustomSchemaIds(x => x.FullName);
            });

            //添加DB组件
            services.AddDapper(Configuration)
                .AddConnectionString("LoginAuthenticationSql")
                .AddConnectionString("LoginAuthenticationSqlReadOnly");

            services.RegisterDependency();
            services.AddHttpClient(Configuration);
            services.AddRedis();
            services.AddSms();

            //JWT Info
            services.Configure<JwtConfig>(Configuration.GetSection("JWT"));
            JwtConfig config = new JwtConfig();
            Configuration.GetSection("JWT").Bind(config);

            //Deployment env enabled authentication
            services.AddLoginAuth(Configuration);

            //日志链路增加CorrelationId
            services.AddApolloErpCorrelationId();
            //请求验证fiter自动注入，与上面的AddApolloErpCorrelationId必须一起使用
            services.AddMvcCore(option =>
            {
                option.Filters.Add(new FilterCorrelationIdAttribute(string.Empty));
            });

            #endregion Extended configuration
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseRouting();
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
                app.UseHsts();
            }
            else
            {
                app.UseHsts();
            }

            var swaggerOnProd = Configuration["EnableSwagger"].EqualsIgnoreCase(CommonConstant.TrueStr);
            if (swaggerOnProd)
            {
                app.UseSwagger(AssemblyName, Configuration, dev: true);
            }

            //Deployment env enabled authentication
            app.UseLoginAuth(Configuration);
            //开启CorrelationId中间件
            app.UseApolloErpCorrelationId();
            app.UseCors("AllowCors");
            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=ZHome}/{action=Index}");
            });
            //app.UseEndpoints(endpoints =>
            //{
                //endpoints.MapControllers();
            //    endpoints.MapControllerRoute("default", "{controller=ZHome}/{action=Index}");
            //});
        }
    }
}
