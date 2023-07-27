using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ae.AccountAuthority.Service.Client.Clients.ShopServer;
using Ae.AccountAuthority.Service.Core.Interfaces;
using Ae.AccountAuthority.Service.Dal.Repositorys.DAL;
using Ae.AccountAuthority.Service.Dal.Repositorys.IDAL;
using Ae.AccountAuthority.Service.Imp.Services;
using System.Net.Http;
using Ae.AccountAuthority.Service.Common.Extension;

namespace Ae.AccountAuthority.Service
{
    public static class DependencyConfig
    {
        public static void RegisterDependency(this IServiceCollection services)
        {
            services.RegisterService(new[]
            {
                "Ae.AccountAuthority.Service.Dal",
                "Ae.AccountAuthority.Service.Imp",
                "Ae.AccountAuthority.Service.Client"
            });
            //#region 外部项目服务类IoC配置

            //services.TryAddScoped<TestClient, TestClient>();

            //#endregion 外部项目服务类IoC配置

            //#region 项目内部服务IoC配置

            //services.TryAddScoped<ITestRespository, TestRespository>();
            //services.TryAddScoped<ITestService, TestService>();

            //services.TryAddScoped<IApplicationRepository, ApplicationRepository>();
            //services.TryAddScoped<IApplicationService, ApplicationService>();

            //services.TryAddScoped<IRoleRepository, RoleRepository>();
            //services.TryAddScoped<IRoleService, RoleService>();
            //services.TryAddScoped<IRoleAuthorityRepository, RoleAuthorityRepository>();

            //services.TryAddScoped<IAuthorityRepository, AuthorityRepository>();
            //services.TryAddScoped<IAuthorityService, AuthorityService>();

            //#endregion 项目内部服务IoC配置
        }

        public static void AddHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            #region 外部项目服务Domain配置

            #endregion 外部项目服务Domain配置
        }

    }
}
