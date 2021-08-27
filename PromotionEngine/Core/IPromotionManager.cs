using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Core
{
    public interface IPromotionManager
    {
        void ApplyPromotion(Order order);
    }
}
