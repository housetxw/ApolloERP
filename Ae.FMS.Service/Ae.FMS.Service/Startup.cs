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
using ApolloErp.Log;
using Ae.FMS.Service.Extension;
using Ae.FMS.Service.Client.Clients;
using ApolloErp.Component.Http;
using Ae.FMS.Service.Filters;

namespace Ae.FMS.Service
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
        { //配置跨域处理
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
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("v1", new Info { Title = "My WebApi Document", Version = "v1" });
            //});

            // Swagger
            services.AddSwagger(AssemblyName, Configuration, c =>
            {
                c.CustomSchemaIds(x => x.FullName);
            });

            //添加DB组件
            services.AddDapper(Configuration)
                .AddConnectionString("FMSSql")
                .AddConnectionString("FMSSqlReadOnly").
                AddConnectionString("ShopPurchaseSql").
                AddConnectionString("ShopPurchaseSqlReadOnly");

            services.AddNlog();
            services.TryAddScoped<OrderClient, OrderClient>();
            services.TryAddScoped<ShopManageClient, ShopManageClient>();
            services.TryAddScoped<ProductClient, ProductClient>();
            services.TryAddScoped<IApolloErpWMSClient, ApolloErpWmsClient>();
            

            services.AddHttpClient("OrderServer", Configuration["OrderServer:Domain"]);
            services.AddHttpClient("ShopManageServer", Configuration["ShopManageServer:Domain"]);
            services.AddHttpClient("ShopOrderServer", Configuration["ShopOrderServer:Domain"]);
            services.AddHttpClient("ProductServer", Configuration["ProductServer:Domain"]);
            services.AddHttpClient("ApolloErpWMSServer", Configuration["ApolloErpWMSServer:Domain"]);

            //    services.AddHttpClient("StockServer", Configuration["StockServer:Domain"]);
            services.RegisterService(new string[] {

                "Ae.FMS.Service.Dal",
                "Ae.FMS.Service.Imp"
            });

            //日志链路增加CorrelationId
            services.AddApolloErpCorrelationId();
            //请求验证fiter自动注入，与上面的AddApolloErpCorrelationId必须一起使用
            services.AddMvcCore(option =>
            {
                option.Filters.Add(new FilterCorrelationIdAttribute(string.Empty));
            });
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
            }
            else
            {
                app.UseHsts();

                app.UseSwagger(AssemblyName, Configuration);
            };

            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            //});

            //开启CorrelationId中间件
            app.UseApolloErpCorrelationId();
            app.UseCors("AllowCors");
            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}");
            });


        }
    }
}
