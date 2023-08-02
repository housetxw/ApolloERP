using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ApolloErp.Log;
using System.Net.Http;
using Ae.B.User.Api.Client.Clients;

namespace Ae.B.User.Api.Extension
{
    /// <summary>
    /// 开始文件扩展
    /// </summary>
    public static class StartupHelper
    {
        /// <summary>
        ///  新增服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClientCorrelationId("UserServer", configuration["UserServer:Domain"]);
            services.AddHttpClientCorrelationId("VehicleServer", configuration["VehicleServer:Domain"]);
            services.AddHttpClientCorrelationId("ReceiveServer", configuration["ReceiveServer:Domain"]);
        }

        /// <summary>
        /// 添加依赖注入对象
        /// </summary>
        /// <param name="services"></param>
        public static void AddDependency(this IServiceCollection services)
        {
            services.AddNlog();
            services.AddScoped<UserClient, UserClient>();
            services.AddScoped<VehicleClient, VehicleClient>();
            services.AddScoped<ReserveClient, ReserveClient>();
            //指定需要注入程序集数组 
            services.RegisterService(new string[]
            {
                "Ae.B.User.Api.Imp"
            });
        }
    }
}
