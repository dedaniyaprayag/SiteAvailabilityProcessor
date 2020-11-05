using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SiteAvailabilityProcessor.Config;
using SiteAvailabilityProcessor.Infrastructure;
using SiteAvailabilityProcessor.Models;
using SiteAvailabilityProcessor.Provider;
using System.Text;
using System.Threading.Tasks;

namespace SiteAvailabilityProcessor
{
    /// <summary>
    /// RabbitMqListner Class
    /// </summary>
    public class RabbitMqListner : IRabbitMqListner
    {
        private readonly IRabbitMqConfiguration _rabbitMqConfiguration;
        private readonly IRabbitMqConnection _rabbitMqConnection;
        private readonly IDbProvider _dbProvider;
        private readonly ISiteAvailablityProvider _siteAvailablityProvider;
        private IModel _channel;
        public RabbitMqListner(IRabbitMqConnection rabbitMqConnection, IRabbitMqConfiguration rabbitMqConfiguration,
            IDbProvider dbProvider, ISiteAvailablityProvider siteAvailablityProvider)
        {
            _rabbitMqConfiguration = rabbitMqConfiguration;
            _rabbitMqConnection = rabbitMqConnection;
            _dbProvider = dbProvider;
            _siteAvailablityProvider = siteAvailablityProvider;
            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {
            _channel = _rabbitMqConnection.GetRabbitMqConnection().CreateModel();
            _channel.QueueDeclare(queue: _rabbitMqConfiguration.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        /// <summary>
        /// Handling the mesasge by checking the site availability and storing the sote hostory in Postgresql
        /// </summary>
        /// <param name="siteModel">Model class for Site</param>
        /// <returns></returns>
        private async Task HandleMessageAsync(SiteDto siteModel)
        {
            var status = _siteAvailablityProvider.CheckSiteAvailablity(siteModel);
            siteModel.Status = status;
            await _dbProvider.InsertAsync(siteModel);
        }

        /// <summary>
        /// Message Queue Listner
        /// </summary>
        /// <returns></returns>
        public Task MessageQueueListner()
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            _channel.BasicConsume(_rabbitMqConfiguration.QueueName, true, consumer);
            consumer.Received += async (o, a) =>
            {
                var content = Encoding.UTF8.GetString(a.Body.ToArray());
                var siteModel = JsonConvert.DeserializeObject<SiteDto>(content);

                await HandleMessageAsync(siteModel);
            };
            return Task.CompletedTask;
        }
    }
}