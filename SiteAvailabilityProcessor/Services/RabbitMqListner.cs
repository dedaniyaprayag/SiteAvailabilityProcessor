using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SiteAvailabilityProcessor.Config;
using SiteAvailabilityProcessor.Infrastructure;
using SiteAvailabilityProcessor.Models;
using SiteAvailabilityProcessor.Provider;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SiteAvailabilityProcessor
{
    public class RabbitMqListner : IRabbitMqListner
    {
        private readonly IRabbitMqConfiguration _rabbitMqConfiguration;
        private readonly IRabbitMqConnection _rabbitMqConnection;
        private readonly IDbProvider _dbProvider;
        private readonly ISiteAvailablityProvider _siteAvailablityProvider;
        private IModel _channel;
        public RabbitMqListner(IRabbitMqConnection rabbitMqConnection, IRabbitMqConfiguration rabbitMqConfiguration, 
            IDbProvider dbProvider,ISiteAvailablityProvider siteAvailablityProvider)
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

        private async Task HandleMessageAsync(SiteDto siteModel)
        {
            var status = await _siteAvailablityProvider.CheckSiteAvailablityAsync(siteModel);
            siteModel.Status = status;
            await _dbProvider.InsertAsync(siteModel);
        }

        public Task MessageQueueListner()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var siteModel = JsonConvert.DeserializeObject<SiteDto>(content);

                await HandleMessageAsync(siteModel);

                _channel.BasicAck(ea.DeliveryTag, true);
            };
            _channel.BasicConsume(_rabbitMqConfiguration.QueueName, true, consumer);

            return Task.CompletedTask;
        }
    }
}