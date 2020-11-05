using SiteAvailabilityProcessor.Models;
using System.Threading.Tasks;

namespace SiteAvailabilityProcessor.Provider
{
    public interface ISiteAvailablityProvider
    {
        bool CheckSiteAvailablity(SiteDto siteModel);
    }
}
