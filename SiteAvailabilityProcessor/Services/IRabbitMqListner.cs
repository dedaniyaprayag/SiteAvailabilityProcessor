using Microsoft.ApplicationInsights;
using System.Threading.Tasks;

namespace SiteAvailabilityProcessor
{
    public interface IRabbitMqListner
    {
        Task MessageQueueListner(TelemetryClient client);
    }
}