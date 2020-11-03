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
        public void RunAsync()
        {
            _rabbitMqListner.MessageQueueListner();
        }
    }
}
