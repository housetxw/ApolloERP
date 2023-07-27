using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using System.Reflection;
using ApolloErp.Web.Swagger;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using Ae.Account.Api.Common.Constant;
using Ae.Account.Api.Common.Extension;
using ApolloErp.Component.Http;
using Ae.Account.Api.Filters;

namespace Ae.Account.Api
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
            #region .Net Core configuration

            //配置跨域处理
            services.AddCors(options =>
            {
                options.AddPolicy("AllowCors", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials(); //指定处理cookie
                });
            });

            services.AddMvc()
                .AddJsonOptions(options => { options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddRouting(options => options.LowercaseUrls = true);

            #endregion .Net Core configuration

            #region Extended configuration

            services.AddNlog();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            //Swagger generation services to our container.
            services.AddSwagger(AssemblyName, Configuration, c =>
            {
                c.CustomSchemaIds(x => x.FullName);

                ////The generated Swagger JSON file will have these properties.
                //c.SwaggerDoc("v1", new Info
                //{
                //    Title = "Swagger XML Api Demo",
                //    Version = "v1",
                //});

                var xmlFile = "Rg.M.AccountAuthority.WebApi.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.RegisterDependency();
            services.AddHttpClient(Configuration);

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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseCors("AllowCors");

            app.UseLoginAuth(Configuration);
            //开启CorrelationId中间件
            app.UseApolloErpCorrelationId();
            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=ZHome}/{action=Index}");
            });
        }
    }
}