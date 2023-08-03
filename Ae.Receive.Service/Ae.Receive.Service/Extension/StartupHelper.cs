using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Ae.Receive.Service.Extension
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
            services.AddHttpClientCorrelationId("VehicleServer", configuration["VehicleServer:Domain"]);
            services.AddHttpClientCorrelationId("OrderServer", configuration["OrderServer:Domain"]);
            services.AddHttpClientCorrelationId("ShopManageServer", configuration["ShopManageServer:Domain"]);
            services.AddHttpClientCorrelationId("UserServer", configuration["UserServer:Domain"]);
            services.AddHttpClientCorrelationId("ConsumerOrderServer", configuration["ConsumerOrderServer:Domain"]);
            services.AddHttpClientCorrelationId("BaoYangServer", configuration["BaoYangServer:Domain"]);
            services.AddHttpClientCorrelationId("ReverseServer", configuration["ReverseServer:Domain"]);
            services.AddHttpClientCorrelationId("ShopOrderServer", configuration["ShopOrderServer:Domain"]);
            services.AddHttpClientCorrelationId("ProductServer", configuration["ProductServer:Domain"]);
        }
    }
}
