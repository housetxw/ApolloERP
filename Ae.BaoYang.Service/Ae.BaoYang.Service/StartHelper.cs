using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ApolloErp.Log;
using Ae.BaoYang.Service.Client.Clients;
using Ae.BaoYang.Service.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ApolloErp.Redis;

namespace Ae.BaoYang.Service
{
    /// <summary>
    /// 开始文件帮助文件
    /// </summary>
    public static class StartHelper
    {
        /// <summary>
        /// 添加依赖注入对象
        /// </summary>
        /// <param name="services"></param>
        public static void AddDependency(this IServiceCollection services)
        {
            services.AddNlog();
            services.AddRedis();

            services.AddScoped<VehicleClient, VehicleClient>();
            services.AddScoped<ShopManageClient, ShopManageClient>();
            services.AddScoped<UserClient, UserClient>();
            services.AddScoped<StockClient, StockClient>();
            services.AddScoped<ProductClient, ProductClient>();
            services.AddScoped<OrderClient, OrderClient>();
            //指定需要注入程序集数组 
            services.RegisterService(new string[]
            {
                "Ae.BaoYang.Service.Dal", "Ae.BaoYang.Service.Imp"
            });
        }

        /// <summary>
        /// 添加ClientDomain
        /// </summary>
        public static void AddClient(this IConfiguration configuration, IServiceCollection services)
        {
            services.AddHttpClientCorrelationId("VehicleServer", configuration["VehicleServer:Domain"]);
            services.AddHttpClientCorrelationId("ShopManageServer", configuration["ShopManageServer:Domain"]);
            services.AddHttpClientCorrelationId("ProductServer", configuration["ProductServer:Domain"]);
            services.AddHttpClientCorrelationId("UserServer", configuration["UserServer:Domain"]);
            services.AddHttpClientCorrelationId("StockServer", configuration["StockServer:Domain"]);
            services.AddHttpClientCorrelationId("OrderServer", configuration["OrderServer:Domain"]);
        }
    }
}
