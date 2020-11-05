using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using SiteAvailabilityProcessor.Config;
using System;
using SiteAvailabilityProcessor.Infrastructure;
using SiteAvailabilityProcessor.Provider;
using System.Net.Http;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;

namespace SiteAvailabilityProcessor
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            // create service collection
            var services = ConfigureServices();
            TelemetryConfiguration configuration = TelemetryConfiguration.CreateDefault();
            configuration.InstrumentationKey = "46713698-7ca1-46bf-9b00-e7e11da851a2";
            var _client = new TelemetryClient(configuration);
            var serviceProvider = services.BuildServiceProvider();

            // calls the Run method in App, which is replacing Main
            await serviceProvider.GetService<App>().RunAsync(_client);

            Console.ReadLine();

        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var config = LoadConfiguration();
            services.AddSingleton(config);
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
