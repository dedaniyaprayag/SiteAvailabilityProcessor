using Microsoft.Extensions.Configuration;
using System;

namespace SiteAvailabilityProcessor
{
    public class App
    {
        private readonly IConfiguration _config;
        private readonly IRabbitMqListner _rabbitMqListner;
        public App(IConfiguration config,IRabbitMqListner rabbitMqListner)
        {
            _config = config;
            _rabbitMqListner = rabbitMqListner;
        }
        public void Run()
        {
            _rabbitMqListner.MessageQueueListner();
        }
    }
}
