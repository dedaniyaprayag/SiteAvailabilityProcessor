using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SiteAvailabilityProcessor.Config;
using SiteAvailabilityProcessor.Infrastructure;
using SiteAvailabilityProcessor.Models;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SiteAvailabilityProcessor
{
    public class RabbitMqListner : IRabbitMqListner
    {
        private readonly IRabbitMqConfiguration _rabbitMqConfiguration;
        private readonly IRabbitMqConnection _rabbitMqConnection;
        private IModel _channel;
        public RabbitMqListner(IRabbitMqConnection rabbitMqConnection, IRabbitMqConfiguration rabbitMqConfiguration)
        {
            _rabbitMqConfiguration = rabbitMqConfiguration;
            _rabbitMqConnection = rabbitMqConnection;
            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {
            _channel = _rabbitMqConnection.GetRabbitMqConnection().CreateModel();
            _channel.QueueDeclare(queue: _rabbitMqConfiguration.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }
        //protected override Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    stoppingToken.ThrowIfCancellationRequested();

        //    var consumer = new EventingBasicConsumer(_channel);
        //    consumer.Received += (ch, ea) =>
        //    {
        //        var content = Encoding.UTF8.GetString(ea.Body.ToArray());
        //        var siteModel = JsonConvert.DeserializeObject<SiteDto>(content);

        //        HandleMessage(siteModel);

        //       // _channel.BasicAck(ea.DeliveryTag, true);
        //    };
        //    _channel.BasicConsume(_rabbitMqConfiguration.QueueName, true, consumer);

        //    return Task.CompletedTask;
        //}

        private void HandleMessage(SiteDto updateCustomerFullNameModel)
        {
            throw new NotImplementedException();
        }

        public Task MessageQueueListner()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var siteModel = JsonConvert.DeserializeObject<SiteDto>(content);

                HandleMessage(siteModel);

                _channel.BasicAck(ea.DeliveryTag, true);
            };
            _channel.BasicConsume(_rabbitMqConfiguration.QueueName, true, consumer);

            return Task.CompletedTask;
        }
    }
}