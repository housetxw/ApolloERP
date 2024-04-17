using System;
using System.Text;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using ApolloErp.Log;
using ApolloErp.SkyWalking;

namespace Ae.Shop.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            ApolloErpSkyBuilder.ConfigureSkyWalking();
            var logger = ApolloErpNLogBuilder.ConfigureNLog();
            try
            {
                //logger.Info("init shop.api");
                //logger.Debug("init main");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                //NLog: catch setup errors
                logger.Error(ex, "Stopped shop.api because of exception");
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
