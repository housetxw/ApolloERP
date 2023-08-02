using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Ae.B.FMS.Api.Extension
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
            services.AddHttpClientCorrelationId("FMSServer", configuration["FMSServer:Domain"]);

            services.AddHttpClientCorrelationId("BasicDataServer", configuration["BasicDataServer:Domain"]);
            services.AddHttpClientCorrelationId("ShopManageServer", configuration["ShopManageServer:Domain"]);
            

        }
    }
}
