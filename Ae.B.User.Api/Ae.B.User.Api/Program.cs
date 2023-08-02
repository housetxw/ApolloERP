using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using ApolloErp.Log;


namespace Ae.B.User.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = ApolloErpNLogBuilder.ConfigureNLog();
            try
            {
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
