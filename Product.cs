using Newtonsoft.Json;
using System;


namespace OrderTransfer
{
    internal class Product
    {
        public int Order_id { get; set; }
        public string Product_id { get; set;}

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
