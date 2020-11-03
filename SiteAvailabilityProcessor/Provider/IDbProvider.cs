using SiteAvailabilityProcessor.Models;
using System.Threading.Tasks;

namespace SiteAvailabilityProcessor.Provider
{
    public interface IDbProvider
    {
        Task<bool> InsertAsync(SiteDto site);
    }
}
