using PromotionEngine.Models;
using System;

namespace PromotionEngine.Core
{
    public class PromotionService
    {
        private IPromotionManager _promotionManager;

        public PromotionService(IPromotionManager promotionManager)
        {
            _promotionManager = promotionManager;
        }

        public void ApplyPromotion(Order order)
        {
            _promotionManager.ApplyPromotion(order);
        }
    }
}
