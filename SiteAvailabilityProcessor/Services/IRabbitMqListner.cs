using System.Threading.Tasks;

namespace SiteAvailabilityProcessor
{
    public interface IRabbitMqListner
    {
        void MessageQueueListner();
    }
}