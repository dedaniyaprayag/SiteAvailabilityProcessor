using RabbitMQ.Client;
using SiteAvailabilityProcessor.Config;

namespace SiteAvailabilityProcessor.Infrastructure
{
    public class RabbitMqConnection : IRabbitMqConnection
    {
        private readonly IConnection connection;

        public RabbitMqConnection(IRabbitMqConfiguration rabbitMqConfiguration)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = rabbitMqConfiguration.Hostname,
                Port = rabbitMqConfiguration.Port,
                UserName = rabbitMqConfiguration.UserName,
                Password = rabbitMqConfiguration.Password
            };

            connection = connectionFactory.CreateConnection();
        }

        public IConnection GetRabbitMqConnection()
        {
            return connection;
        }
    }
}
