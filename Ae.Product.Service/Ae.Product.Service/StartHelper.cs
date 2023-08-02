using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ae.Product.Service
{
    /// <summary>
    /// 开始文件帮助文件
    /// </summary>
    public static class StartHelper
    {
        /// <summary>
        /// 添加ClientDomain
        /// </summary>
        public static void AddClient(this IConfiguration configuration, IServiceCollection services)
        {
            services.AddHttpClientCorrelationId("ShopManageServer", configuration["ShopManageServer:Domain"]);
            services.AddHttpClientCorrelationId("BaoYangServer", configuration["BaoYangServer:Domain"]);
        }
    }
}
