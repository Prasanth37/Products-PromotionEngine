using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionEngine.Models
{
    public class Promotion : Order
    {
        public IEnumerable<Item> Validate(Order order, IEnumerable<Item> validatedItems)
        {
            var existingItems = new List<Item>();
            if (Items == null || Items.Count < 1)
                return existingItems;

            foreach (var promotionItem in Items)
            {
                var foundItem = validatedItems.FirstOrDefault(x => x.Id == promotionItem.Id) ??
                  order.Items.FirstOrDefault(x => x.Id == promotionItem.Id);
                if (foundItem == null || foundItem.Quantity < promotionItem.Quantity)
                    return null;

                if (!existingItems.Any(x => x.Id == foundItem.Id))
                    existingItems.Add(new Item(foundItem));
            }

            ApplyPromotion(order, existingItems);

            return existingItems;
        }

        private void ApplyPromotion(Order order, List<Item> foundItems)
        {
            var found = foundItems.Count() > 0;
            if (found)
            {
                do
                {
                    order.TotalAmount += TotalAmount;
                    foreach (var promotionItem in Items)
                    {
                        var item = foundItems.FirstOrDefault(x => x.Id == promotionItem.Id);
                        if (item != null)
                        {
                            item.Quantity -= promotionItem.Quantity;
                            if (found)
                                found = item.Quantity >= promotionItem.Quantity;
                        }
                    }
                } while (found);
            }
        }
    }
}
