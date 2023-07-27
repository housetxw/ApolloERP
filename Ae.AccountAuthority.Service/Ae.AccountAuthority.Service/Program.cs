using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using ApolloErp.Log;
using ApolloErp.SkyWalking;

namespace Ae.AccountAuthority.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ApolloErpSkyBuilder.ConfigureSkyWalking();

            //Loading NLog.config
            var logger = ApolloErpNLogBuilder.ConfigureNLog();

            try
            {
                logger.Info("Ae.AccountAuthority.Service Application Starting...");


                CreateWebHostBuilder(args).Build().Run();


                logger.Info("Ae.AccountAuthority.Service Application Start Succeed");
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.Error(ex, "Ae.AccountAuthority.Service log record stopped, because of exception in Program.Main");

                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
                logger.Info("Ae.AccountAuthority.Service NLog.LogManager.Shutdown() Executed");
            }

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ApolloErpConfigureLogging();
    }
}
