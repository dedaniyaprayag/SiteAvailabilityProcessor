using System.Threading.Tasks;

namespace SiteAvailabilityProcessor
{
    public interface IRabbitMqListner
    {
        Task MessageQueueListner();
    }
}