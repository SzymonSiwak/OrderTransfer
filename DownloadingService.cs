using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OrderTransfer
{
    public interface IDownloadingService
    {
        Task<SourceOrder> GetFaireOrder();
    }
    internal class DownloadingService : IDownloadingService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DownloadingService> _logger;

        public DownloadingService(IConfiguration config, ILogger<DownloadingService> logger)
        {
            _configuration = config;
            _logger = logger;
        }
        public async Task<SourceOrder> GetFaireOrder()
        {
            string url = "https://www.faire.com/api/v1/orders/1024"; 

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-FAIRE-ACCESS-TOKEN", _configuration["FaireAccessToken"]);

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var deserializiedResponse = JsonConvert.DeserializeObject<SourceOrder>(responseBody);
                    return deserializiedResponse;
                }
                else
                {
                    _logger.LogError("Fail at to get order");
                    return new SourceOrder();
                }
            }
        }
    }
}
