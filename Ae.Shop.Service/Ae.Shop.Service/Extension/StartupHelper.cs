using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Ae.Shop.Service.Extension
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
            services.AddHttpClientCorrelationId("BasicDataServer", configuration["BasicDataServer:Domain"]);
            services.AddHttpClientCorrelationId("ProductServer", configuration["ProductServer:Domain"]);
            services.AddHttpClientCorrelationId("AccountAuthorityServer", configuration["AccountAuthorityServer:Domain"]);
            services.AddHttpClientCorrelationId("OrderCommentServer", configuration["OrderCommentServer:Domain"]);
            services.AddHttpClientCorrelationId("OrderServer", configuration["OrderServer:Domain"]);
            services.AddHttpClientCorrelationId("BaoYangServer", configuration["BaoYangServer:Domain"]);
            services.AddHttpClientCorrelationId("UserServer", configuration["UserServer:Domain"]);
            services.AddHttpClientCorrelationId("ReceiveServer", configuration["ReceiveServer:Domain"]);
            services.AddHttpClientCorrelationId("VehicleServer", configuration["VehicleServer:Domain"]);
            services.AddHttpClientCorrelationId("ActivityServer", configuration["ActivityServer:Domain"]);
            services.AddHttpClientCorrelationId("FileUploadServer", configuration["FileUploadServer:Domain"]);
            services.AddHttpClientCorrelationId("ShopOrderServer", configuration["ShopOrderServer:Domain"]);
            services.AddHttpClientCorrelationId("PurchaseServer", configuration["PurchaseServer:Domain"]);
            
        }
    }
}
