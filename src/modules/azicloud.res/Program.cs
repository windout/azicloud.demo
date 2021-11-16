using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;

namespace azicloud.res
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }       

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                    {
                        options.Listen(IPAddress.Any, 5001, listenOptions =>
                        {
                            listenOptions.Protocols = HttpProtocols.Http2;
                            
                        });
                    });
                    webBuilder.UseStartup<Startup>();
                });

        private static (int httpPort, int grpcPort) GetDefinedPorts(IConfiguration config)
        {
            var port = config.GetValue("PORT", -1);
            if (port == -1)
            {
                var aspnetcoreUris = config.GetValue("APSNETCORE_URLS", "");
                if (!string.IsNullOrEmpty(aspnetcoreUris))
                {
                    if (Uri.TryCreate(aspnetcoreUris, UriKind.Absolute, out Uri uri))
                    {
                        port = uri.Port;
                    }
                }
            }
            var grpcPort = config.GetValue("GRPC_PORT",port+1);
            return (port, grpcPort);
        }
    }
}
