using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using ApolloErp.Log;
using ApolloErp.SkyWalking;
using Microsoft.Extensions.Hosting;

namespace Ae.B.Login.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Loading NLog.config
            var logger = ApolloErpNLogBuilder.ConfigureNLog();

            ApolloErpSkyBuilder.ConfigureSkyWalking();

            try
            {
                logger.Info("Ae.B.Login.Api Application Starting...");


                CreateWebHostBuilder(args).Build().Run();
                //CreateHostBuilder(args).Build().Run();


                logger.Info("Ae.B.Login.Api Application Start Succeed");
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.Error(ex, "Ae.B.Login.Api log record stopped, because of exception in Program.Main");

                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
                //logger.Info("Ae.B.Login.Api NLog.LogManager.Shutdown() Executed");
            }

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ApolloErpConfigureLogging();
        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}
