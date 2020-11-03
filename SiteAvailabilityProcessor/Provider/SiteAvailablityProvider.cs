using SiteAvailabilityProcessor.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace SiteAvailabilityProcessor.Provider
{
    public class SiteAvailablityProvider : ISiteAvailablityProvider
    {
        private readonly HttpClient _httpClient;
        public SiteAvailablityProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> CheckSiteAvailablityAsync(SiteDto siteModel)
        {
            var result = await _httpClient.GetAsync(siteModel.Site);
            return result.IsSuccessStatusCode;
        }
    }
}
