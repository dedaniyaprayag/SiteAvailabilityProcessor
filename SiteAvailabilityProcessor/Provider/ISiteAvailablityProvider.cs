using Microsoft.ApplicationInsights;
using SiteAvailabilityProcessor.Models;

namespace SiteAvailabilityProcessor.Provider
{
    public interface ISiteAvailablityProvider
    {
        bool CheckSiteAvailablity(SiteDto siteModel, TelemetryClient client);
    }
}
