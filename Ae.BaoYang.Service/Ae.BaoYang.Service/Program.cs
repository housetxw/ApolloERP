using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ApolloErp.Log;
using ApolloErp.SkyWalking;

namespace Ae.BaoYang.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ApolloErpSkyBuilder.ConfigureSkyWalking();
            var logger = ApolloErpNLogBuilder.ConfigureNLog();
            try
            {
                logger.Debug("init main");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().ApolloErpConfigureLogging();
    }
}
