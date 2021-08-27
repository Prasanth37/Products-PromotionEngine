using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionEngine.Core;
using PromotionEngine.Models;
using System.Collections.Generic;

namespace PromotionEngine.Test
{
    [TestClass]
    public class PromotionEngineTests
    {
        static IPromotionManager _promotionManager;
        PromotionService _promotionService;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            IEnumerable<Price> priceList = new List<Price>
                                        {
                                            new Price { ItemId = 'A', UnitPrice = 50 },
                                            new Price { ItemId = 'B', UnitPrice = 30 },
                                            new Price { ItemId = 'C', UnitPrice = 20 },
                                            new Price { ItemId = 'D', UnitPrice = 15 }
                                        };

            IEnumerable<Promotion> promotions = new List<Promotion>
                                                {
                                                    new Promotion {
                                                          Items = new List<Item>
                                                          {
                                                            new Item { Id = 'A', Quantity = 3 }
                                                          },
                                                          TotalAmount = 130
                                                    },
                                                    new Promotion {
                                                          Items = new List<Item>
                                                          {
                                                            new Item { Id = 'B', Quantity = 2 }
                                                          },
                                                          TotalAmount = 45
                                                    },
                                                    new Promotion {
                                                          Items = new List<Item>
                                                          {
                                                            new Item { Id = 'C', Quantity = 1 },
                                                            new Item { Id = 'D', Quantity = 1 }
                                                          },
                                                          TotalAmount = 30
                                                    }
                                                };

            _promotionManager = new PromotionManager(priceList, promotions);
        }

        [TestMethod]
        public void ApplyPromotion_Test_SingleItems_Returns_ActualTotal()
        {
            var order = new Order
            {
                Items = new List<Item>
                {
                    new Item { Id = 'A', Quantity = 1 },
                    new Item { Id = 'B', Quantity = 1 },
                    new Item { Id = 'C', Quantity = 1 }
                }
            };

            _promotionManager.ApplyPromotion(order);
            Assert.IsTrue(order.TotalAmount == 100);
        }

        [TestMethod]
        public void ApplyPromotion_Test_MultipleItems_AandB_Returns_TotalWithPromotion()
        {
            var order = new Order
            {
                Items = new List<Item>
                {
                    new Item { Id = 'A', Quantity = 5 },
                    new Item { Id = 'B', Quantity = 5 },
                    new Item { Id = 'C', Quantity = 1 }
                }
            };

            _promotionManager.ApplyPromotion(order);
            Assert.IsTrue(order.TotalAmount == 370);
        }

        [TestMethod]
        public void ApplyPromotion_Test_AllItems_Returns_TotalWithPromotion()
        {
            var order = new Order
            {
                Items = new List<Item>
                {
                    new Item { Id = 'A', Quantity = 3 },
                    new Item { Id = 'B', Quantity = 5 },
                    new Item { Id = 'C', Quantity = 1 },
                    new Item { Id = 'D', Quantity = 1 }
                }
            };

            _promotionManager.ApplyPromotion(order);
            Assert.IsTrue(order.TotalAmount == 280);
        }

        [TestMethod]
        public void ApplyPromotion_Test_Items_Returns_TotalWithPromotion1()
        {
            var order = new Order
            {
                Items = new List<Item>
                {
                    new Item { Id = 'A', Quantity = 3 },
                    new Item { Id = 'B', Quantity = 5 },
                    new Item { Id = 'C', Quantity = 1 },
                    new Item { Id = 'D', Quantity = 2 }
                }
            };

            _promotionManager.ApplyPromotion(order);
            Assert.IsTrue(order.TotalAmount == 295);
        }
    }
}
