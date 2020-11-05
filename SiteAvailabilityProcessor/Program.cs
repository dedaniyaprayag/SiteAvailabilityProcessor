﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using SiteAvailabilityProcessor.Config;
using System;
using SiteAvailabilityProcessor.Infrastructure;
using SiteAvailabilityProcessor.Provider;
using System.Net.Http;

namespace SiteAvailabilityProcessor
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            // create service collection
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            // calls the Run method in App, which is replacing Main
            await serviceProvider.GetService<App>().RunAsync();

            Console.ReadLine();

        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var config = LoadConfiguration();
            services.AddSingleton(config);
          //  services.AddApplicationInsightsTelemetry();
            services.AddTransient<IRabbitMqListner, RabbitMqListner>();
            services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
            services.AddSingleton<IRabbitMqConfiguration>(config.GetSection("RabbitMq").Get<RabbitMqConfiguration>());
            services.AddSingleton<IPostgreSqlConfiguration>(config.GetSection("PostgreSql").Get<PostgreSqlConfiguration>());
            services.AddTransient<App>();
            services.AddHttpClient<ISiteAvailablityProvider, SiteAvailablityProvider>();
            services.AddTransient<IDbProvider, PostgresSqlProvider>();
            return services;
        }

        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}
