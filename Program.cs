using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace OcelotApiGw
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddEnvironmentVariables()
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile("configuration.json", optional: false, reloadOnChange: true)
          .Build();
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
             .UseStartup<Startup>()
            .UseNLog()
            .UseConfiguration(Configuration);
    }
}
