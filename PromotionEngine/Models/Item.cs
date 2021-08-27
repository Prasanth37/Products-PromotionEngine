using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Models
{
    public class Item
    {
        public char Id { get; set; }
        public int Quantity { get; set; }

        public Item() { }

        public Item(Item item)
        {
            Id = item.Id;
            Quantity = item.Quantity;
        }
    }
}
