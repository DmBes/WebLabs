using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ASP.TagHelpers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace ASP
{
    public class Program
    {
        public static void Main(string[] args)
        {

             CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).ConfigureLogging(log =>
                    {
                        var t = AppDomain.CurrentDomain.BaseDirectory;
                        var t4 = Directory.GetCurrentDirectory();





                        var path = Path.Combine(Directory.GetCurrentDirectory(),
                        "fileInfoLog.txt"); 
                        log.ClearProviders(); 
                        log.AddProvider(new FileLoggerProvider(path));
                        log.AddFilter("Microsoft", LogLevel.None); }
                    )
                .UseStartup<Startup>();
    }
}
