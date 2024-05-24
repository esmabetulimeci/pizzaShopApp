using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Application.Services
{
    public class TcmbService
    {
        private readonly HttpClient _client;

        public TcmbService(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task<decimal> GetExchangeRateAsync(string currencyCode)
        {
            var url = "https://www.tcmb.gov.tr/kurlar/today.xml";

            using (var response = await _client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var xml = await response.Content.ReadAsStringAsync();
                    var xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xml);

                    var node = xmlDoc.SelectSingleNode($"//Currency[@Kod='{currencyCode}']");
                    if (node != null && node.SelectSingleNode("ForexBuying") != null)
                    {
                        var exchangeRateText = node.SelectSingleNode("ForexBuying").InnerText;
                        if (decimal.TryParse(exchangeRateText, out decimal exchangeRate))
                        {
                            return exchangeRate;
                        }
                    }
                }
            }

            throw new Exception("Döviz kuru alınamadı.");
        }

    }
}
