using System.Collections.Generic;

namespace OrderTransfer
{
    public class SourceOrder
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<OrderProduct> Items { get; set; }

    }
}