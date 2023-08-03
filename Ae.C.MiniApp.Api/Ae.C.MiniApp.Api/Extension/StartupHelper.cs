using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Ae.C.MiniApp.Api.Extension
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
            services.AddHttpClientCorrelationId("OrderServer", configuration["OrderServer:Domain"]);
            services.AddHttpClientCorrelationId("CouponServer", configuration["CouponServer:Domain"]);
            services.AddHttpClientCorrelationId("ConsumerOrderServer", configuration["ConsumerOrderServer:Domain"]);
            services.AddHttpClientCorrelationId("ShopOrderServer", configuration["ShopOrderServer:Domain"]);
            services.AddHttpClientCorrelationId("PayServer", configuration["PayServer:Domain"]);
            services.AddHttpClientCorrelationId("VehicleServer", configuration["VehicleServer:Domain"]);
            services.AddHttpClientCorrelationId("ProductServer", configuration["ProductServer:Domain"]);
            services.AddHttpClientCorrelationId("ShopManageServer", configuration["ShopManageServer:Domain"]);
            services.AddHttpClientCorrelationId("BasicDataServer", configuration["BasicDataServer:Domain"]);
            services.AddHttpClientCorrelationId("ReceiveServer", configuration["ReceiveServer:Domain"]);
            services.AddHttpClientCorrelationId("UserServer", configuration["UserServer:Domain"]);
            services.AddHttpClientCorrelationId("BaoYangServer", configuration["BaoYangServer:Domain"]);
            services.AddHttpClientCorrelationId("StockServer", configuration["StockServer:Domain"]);
            services.AddHttpClientCorrelationId("OrderCommentServer", configuration["OrderCommentServer:Domain"]);
            services.AddHttpClientCorrelationId("ReverseServer", configuration["ReverseServer:Domain"]);
            services.AddHttpClientCorrelationId("ActivityServer", configuration["ActivityServer:Domain"]);
        }
    }
}
