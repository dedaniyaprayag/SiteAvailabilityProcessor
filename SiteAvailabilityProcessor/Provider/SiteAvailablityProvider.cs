using Microsoft.ApplicationInsights;
using SiteAvailabilityProcessor.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace SiteAvailabilityProcessor.Provider
{
    public class SiteAvailablityProvider : ISiteAvailablityProvider
    {
        private readonly HttpClient _httpClient;

        public SiteAvailablityProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public bool CheckSiteAvailablity(SiteDto siteModel, TelemetryClient client)
        {
            siteModel.Site = siteModel.Site.Replace("https://", "").Replace("http://", "");
            siteModel.Site = "http://" + siteModel.Site;
            return PingHost(new Uri(siteModel.Site), client) == "Ok";
        }

        public string PingHost(Uri uri, TelemetryClient client)
        {
            string returnMessage = string.Empty;
            IPAddress address = Dns.GetHostAddresses(uri.Host)[0];
            PingOptions pingOptions = new PingOptions(128, true);

            Ping ping = new Ping();
            byte[] buffer = new byte[32];
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    PingReply pingReply = ping.Send(address, 2000, buffer, pingOptions);

                    if (!(pingReply == null))
                    {
                        switch (pingReply.Status)
                        {
                            case IPStatus.Success:
                                returnMessage = "Ok";
                                break;
                            case IPStatus.TimedOut:
                                returnMessage = "Connection has timed out...";
                                break;
                            default:
                                returnMessage = string.Format("Ping failed: {0}", pingReply.Status.ToString());
                                break;
                        }
                    }
                    else
                        returnMessage = "Connection failed for an unknown reason...";
                }
                catch (PingException ex)
                {
                    returnMessage = string.Format("Connection Error: {0}", ex.Message);
                }
                catch (SocketException ex)
                {
                    returnMessage = string.Format("Connection Error: {0}", ex.Message);
                }
            }
            var dict = new Dictionary<string, string>()
            {
                { "site",uri.Host },
                {"AbsoluteUri", uri.AbsoluteUri},
                {"AbsolutePath", uri.AbsolutePath},
                {
                    "message", returnMessage
                }

            };
            client.TrackEvent("siteEvents", dict);
            return returnMessage;
        }
    }
}
