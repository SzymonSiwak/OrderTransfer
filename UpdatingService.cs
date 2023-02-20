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
    public interface IUpdatingService
    {
        Task PostBaselinkerOrder(SourceOrder order);
    }

    internal class UpdatingService : IUpdatingService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UpdatingService> _logger;

        public UpdatingService(IConfiguration config, ILogger<UpdatingService> logger)
        {
            _configuration = config;
            _logger = logger;
        }

        public async Task PostBaselinkerOrder(SourceOrder order)
        {
            foreach (var item in order.Items)
            {
                var product = new Product()
                {
                    Order_id = item.Order_Id,
                    Product_id = item.Product_Id,
                    Name = order.Name
                };
                var productJson = JsonConvert.SerializeObject(product);
                await CreateRequest(productJson);
            }
        }

        private async Task CreateRequest(string productJson)
        {
            string url = "https://api.baselinker.com/connector.php";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-BLToken", _configuration["BaselinkerAccessToken"]);
                client.DefaultRequestHeaders.Add("method", "AddOrderProduct");

                StringContent httpContent = new StringContent(productJson, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, httpContent);

                if (response.IsSuccessStatusCode == false)
                {
                    _logger.LogError("Fail to update");
                }
            }
        }
    }
}