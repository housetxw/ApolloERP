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

namespace Ae.FMS.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ApolloErpSkyBuilder.ConfigureSkyWalking();
            var logger = ApolloErpNLogBuilder.ConfigureNLog();
            try
            {
                //logger.Info("Ae.FMS.Service Application Starting...");
                logger.Debug("init main");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ApolloErpConfigureLogging();
    }
}
