namespace SiteAvailabilityProcessor.Config
{
    public interface IRabbitMqConfiguration
    {
        string Hostname { get; set; }

        string QueueName { get; set; }

        string UserName { get; set; }

        string Password { get; set; }
        int Port { get; set; }
    }
}
