using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ae.B.Login.Api.Common.Extension;

namespace Ae.B.Login.Api
{
    public static class DependencyConfig
    {
        public static void RegisterDependency(this IServiceCollection services)
        {
            //指定需要注入程序集数组 
            services.RegisterService(new string[] {
                "Ae.B.Login.Api.Client" ,
                "Ae.B.Login.Api.Imp",
                "Ae.B.Login.Api.Dal",
            });
        }

        public static void AddHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            #region 外部项目服务Domain配置

            services.AddHttpClientCorrelationId("ShopManageServer", configuration["ShopManageServer:Domain"]);


            #endregion 外部项目服务Domain配置
        }

    }
}
