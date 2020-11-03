namespace SiteAvailabilityProcessor.Config
{
    /// <summary>
    /// RabbitMqConfiguration class
    /// </summary>
    public class RabbitMqConfiguration : IRabbitMqConfiguration
    {
        public string Hostname { get; set; }
        public string QueueName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
    }
}
