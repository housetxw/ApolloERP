using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ae.BasicData.Service.Client.Clients;
using Ae.BasicData.Service.Client.Clients.ShopServer;
using Ae.BasicData.Service.Client.Inteface;
using Ae.BasicData.Service.Common.Extension;
using Ae.BasicData.Service.Core.Interfaces;
using Ae.BasicData.Service.Dal.Repositorys.DAL;
using Ae.BasicData.Service.Dal.Repositorys.IDAL;
using Ae.BasicData.Service.Imp.Services;

namespace Ae.BasicData.Service
{
    public static class DependencyConfig
    {
        public static void RegisterDependency(this IServiceCollection services)
        {
            //指定需要注入程序集数组 
            services.RegisterService(new string[] {
                "Ae.BasicData.Service.Client" ,
                "Ae.BasicData.Service.Imp",
                "Ae.BasicData.Service.Dal",
            });

            //#region 外部项目服务类IoC配置

            //services.TryAddScoped<TestClient, TestClient>();
            //services.TryAddScoped<ITestService, TestService>();
            //services.TryAddScoped<ITencentMapClient, TencentMapClient>();

            //#endregion 外部项目服务类IoC配置

            //#region 项目内部服务IoC配置

            //services.TryAddScoped<IRegionChinaRepository, RegionChinaRepository>();
            //services.TryAddScoped<IRegionChinaService, RegionChinaService>();

            //#endregion 项目内部服务IoC配置
        }

        public static void AddHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            #region 外部项目服务Domain配置

            services.AddHttpClient("TencentMapServer", configuration["TencentMapServer:Domain"]);

            #endregion 外部项目服务Domain配置

            #region 内部项目服务Domain配置

            #endregion 内部项目服务Domain配置
        }
    }
}
