namespace Argone.Core.Extensions
{
    using System;
    using Argone.Core.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureArgone(this IHostBuilder hostBuilder, Action<IApplicationBuilder> configure)
        {
            
            hostBuilder.ConfigureServices(services =>
            {
                services.AddArgone(configure);
            });
            return hostBuilder;
        }

        public static IServiceCollection AddArgone(this IServiceCollection services, Action<IApplicationBuilder> configure)
        {
            configure(new ApplicationBuilder(services));
            return services;
        }
    }
}