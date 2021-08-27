using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Models
{
    public class Order
    {
        public List<Item> Items { get; set; }
        public double TotalAmount { get; set; }
    }
}
