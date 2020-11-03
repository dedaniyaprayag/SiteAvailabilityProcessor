using RabbitMQ.Client;

namespace SiteAvailabilityProcessor.Infrastructure
{
    public interface IRabbitMqConnection
    {
        IConnection GetRabbitMqConnection();
    }
}
