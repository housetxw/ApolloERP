using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ae.Account.Api.Common.Extension;

namespace Ae.Account.Api
{
    public static class DependencyConfig
    {
        public static void RegisterDependency(this IServiceCollection services)
        {
            services.RegisterService(new[]
            {
                "Ae.Account.Api.Client",
                "Ae.Account.Api.Imp"
            });
            //#region 外部项目服务类IoC配置

            //services.TryAddScoped<TestClient, TestClient>();
            //services.TryAddScoped<ITestService, TestService>();

            //services.TryAddScoped<CompanyClient, CompanyClient>();

            //#endregion 外部项目服务类IoC配置

            //#region 项目内部服务IoC配置

            //services.TryAddScoped<ApplicationClient, ApplicationClient>();
            //services.TryAddScoped<IApplicationService, ApplicationService>();

            //services.TryAddScoped<RoleClient, RoleClient>();
            //services.TryAddScoped<IRoleService, RoleService>();

            //services.TryAddScoped<AuthorityClient, AuthorityClient>();
            //services.TryAddScoped<IAuthorityService, AuthorityService>();

            //#endregion 项目内部服务IoC配置
        }

        public static void AddHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            #region 外部项目服务Domain配置

            services.AddHttpClientCorrelationId("ShopManageServer", configuration["ShopManageServer:Domain"]);

            #endregion 外部项目服务Domain配置

            #region 内部项目服务Domain配置

            services.AddHttpClientCorrelationId("AccountAuthorityServer", configuration["AccountAuthorityServer:Domain"]);


            #endregion 内部项目服务Domain配置
        }

    }
}
