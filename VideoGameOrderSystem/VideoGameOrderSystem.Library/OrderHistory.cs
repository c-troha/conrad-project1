using System;
using System.Collections.Generic;
using System.Text;

namespace VideoGameOrderSystem.Library
{
    public class OrderHistory
    {
        public int OrderId { get; set; }

        public IEnumerable<string> ProductName { get; set; }
        public IEnumerable<int> Quantity { get; set; }
        public float OrderTotal { get; set; }
    }
}
