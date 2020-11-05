using Microsoft.ApplicationInsights;
using System.Threading.Tasks;

namespace SiteAvailabilityProcessor
{
    public class App
    {
        private readonly IRabbitMqListner _rabbitMqListner;
        public App(IRabbitMqListner rabbitMqListner)
        {
            _rabbitMqListner = rabbitMqListner;
        }
        public async Task RunAsync(TelemetryClient client)
        {
            await _rabbitMqListner.MessageQueueListner(client);
        }
    }
}
