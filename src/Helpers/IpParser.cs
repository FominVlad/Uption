using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Uption.Models;

namespace Uption.Helpers
{
    public class IpParser
    {
        private string url { get; set; }
        private string accessKey { get; set; }

        public IpParser()
        {
            this.url = "http://api.ipapi.com/";
            this.accessKey = "dfb8052122ca0a66b6d148aaceb45410";
        }

        public async Task<IpLocation> Parse(string ip)
        {
            IpLocation parsedIp = new IpLocation();
            HttpClient httpClient = HttpClientFactory.Create();

            try
            {
                if (string.IsNullOrEmpty(ip))
                {
                    throw new Exception("IP is null or empty!");
                }

                string requestResult = await httpClient.GetStringAsync($"{this.url}{ip}?access_key={this.accessKey}");

                dynamic dynamicParsedIp = JObject.Parse(requestResult);

                parsedIp.Ip = ip;
                parsedIp.Continent = dynamicParsedIp.continent_name;
                parsedIp.Country = dynamicParsedIp.country_name;
                parsedIp.Region = dynamicParsedIp.region_name;
                parsedIp.City = dynamicParsedIp.city;
            }
            catch (Exception ex)
            {

            }

            return parsedIp;
        }
    }
}
