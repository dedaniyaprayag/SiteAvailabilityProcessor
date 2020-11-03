using SiteAvailabilityProcessor.Models;
using System.Threading.Tasks;

namespace SiteAvailabilityProcessor.Provider
{
    public interface ISiteAvailablityProvider
    {
        Task<bool> CheckSiteAvailablityAsync(SiteDto siteModel);
    }
}
