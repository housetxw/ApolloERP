using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Ae.ShopOrder.Service.Extension
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
            services.AddHttpClientCorrelationId("ShopServer", configuration["ShopServer:Domain"]);

            services.AddHttpClientCorrelationId("CustomerOrderServer", configuration["CustomerOrderServer:Domain"]);

            services.AddHttpClientCorrelationId("WMSServer", configuration["WMSServer:Domain"]);

            services.AddHttpClientCorrelationId("OrderServer", configuration["OrderServer:Domain"]);

            services.AddHttpClientCorrelationId("StockServer", configuration["StockServer:Domain"]);

            services.AddHttpClientCorrelationId("ShopStockServer", configuration["ShopStockServer:Domain"]);

            services.AddHttpClientCorrelationId("UserServer", configuration["UserServer:Domain"]);

            services.AddHttpClientCorrelationId("ProductServer", configuration["ProductServer:Domain"]);

            services.AddHttpClientCorrelationId("VehicleServer", configuration["VehicleServer:Domain"]);

            services.AddHttpClientCorrelationId("ShopManageServer", configuration["ShopManageServer:Domain"]);

            services.AddHttpClientCorrelationId("PayServer", configuration["PayServer:Domain"]);

            services.AddHttpClientCorrelationId("ReceiveServer", configuration["ReceiveServer:Domain"]);

            services.AddHttpClientCorrelationId("BaoYangServer", configuration["BaoYangServer:Domain"]);

            services.AddHttpClientCorrelationId("CouponServer", configuration["CouponServer:Domain"]);

            services.AddHttpClientCorrelationId("FmsServer", configuration["FmsServer:Domain"]);
        }
    }
}
