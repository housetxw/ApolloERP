using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ApolloErp.Log;
using Ae.User.Service.Extension;
using System.Net.Http;
namespace Ae.User.Service
{
    /// <summary>
    /// 开始文件帮助文件
    /// </summary>
    public static class StartHelper
    {
        /// <summary>
        /// 添加依赖注入对象
        /// </summary>
        /// <param name="services"></param>
        public static void AddDependency(this IServiceCollection services)
        {
            services.AddNlog();

            //指定需要注入程序集数组 
            services.RegisterService(new string[]
            {
                "Ae.User.Service.Dal", "Ae.User.Service.Imp"
            });
        }

        /// <summary>
        /// 添加ClientDomain
        /// </summary>
        public static void AddClient(this IConfiguration configuration, IServiceCollection services)
        {

        }
    }
}
