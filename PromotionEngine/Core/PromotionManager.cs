using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionEngine.Core
{
    public class PromotionManager : IPromotionManager
    {
        private IEnumerable<Price> _priceList;
        private IEnumerable<Promotion> _promotions;

        public PromotionManager(IEnumerable<Price> priceList, IEnumerable<Promotion> promotions)
        {
            _priceList = priceList;
            _promotions = promotions;
        }

        public void ApplyPromotion(Order order)
        {
            var existingItems = new List<Item>();
            if (_promotions != null && _promotions.Count() > 0)
            {
                foreach (var promotion in _promotions)
                {
                    var validatedItems = promotion.Validate(order, existingItems);
                    UpdateValidatedItems(existingItems, validatedItems);
                }
            }

            ApplyRegularPrice(order, existingItems);
        }

        private void ApplyRegularPrice(Order order, List<Item> existingItems)
        {
            foreach (var item in order.Items)
            {
                var validatedItem = existingItems.FirstOrDefault(x => x.Id == item.Id) ?? item;
                var quantity = validatedItem.Quantity;
                if (quantity > 0)
                    order.TotalAmount += quantity * _priceList.First(x => x.ItemId == item.Id).UnitPrice;
            }
        }

        private static void UpdateValidatedItems(List<Item> existingItems, IEnumerable<Item> validatedItems)
        {
            if (validatedItems == null || validatedItems.Count() < 1)
                return;

            foreach (var item in validatedItems)
                if (!existingItems.Any(x => x.Id == item.Id))
                    existingItems.Add(item);
        }
    }
}
