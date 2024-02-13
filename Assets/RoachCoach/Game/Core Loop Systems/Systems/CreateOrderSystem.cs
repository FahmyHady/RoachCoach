using Entitas;
using System.Collections.Generic;
using static RoachCoach.RoachCoachGameChefMatcher;
using static RoachCoach.RoachCoachGameDelayMatcher;
using static RoachCoach.RoachCoachGameTakingAnOrderMatcher;
using static RoachCoach.RoachCoachGameCharacterMatcher;
using UnityEngine;
using System;
namespace RoachCoach
{
    public class CreateOrderSystem : ReactiveSystem<Game.Entity>
    {
        private readonly GameContext gameContext;
        private readonly ConfigContext configContext;
        private readonly IShopConfig shopConfig;

        public CreateOrderSystem(GameContext gameContext, ConfigContext configContext) : base(gameContext)
        {
            this.gameContext = gameContext;
            this.configContext = configContext;
            this.shopConfig = configContext.GetShopConfig().Value;
        }
        protected override void Execute(List<Game.Entity> entities)
        {
            foreach (var chef in entities)
            {
                chef.RemoveTakingAnOrder();
                CreateOrder(chef.GetRelatedCustomer().RelatedCustomer);
                chef.RemoveRelatedCustomer();
                chef.AddFree();
            }
        }

        protected override bool Filter(Game.Entity entity)
        {
            return !entity.HasDelay() && entity.HasTakingAnOrder();
        }

        protected override ICollector<Game.Entity> GetTrigger(IContext<Game.Entity> context)
        {
            return context.CreateCollector(Game.Matcher.AllOf(Chef, Character, TakingAnOrder).NoneOf(Delay));
        }
        Game.Entity CreateOrder(Game.Entity relatedCustomer)
        {
            var randomOrder = shopConfig.GetOrderData();
            var customerTransform = relatedCustomer.GetTransform();
            var orderPos = customerTransform.position + Vector3.up * 3;
            var orderEntity = gameContext.CreateEntity()
                .AddFree()
                .AddOrder()
                .AddRelatedCustomer(relatedCustomer)
                .AddTransform(orderPos, Quaternion.Euler(35,0,0))
                .AddCommodity(randomOrder, true)
                .AddMoney(GetPrice(randomOrder));
            return orderEntity;
        }

        private int GetPrice((CommodityType, int) randomOrder)
        {
            int priceOfOne = 0;
            switch (randomOrder.Item1)
            {
                case CommodityType.Taco:
                    priceOfOne = shopConfig.TacoPrice;
                    break;
                case CommodityType.Soda:
                    priceOfOne = shopConfig.SodaPrice;
                    break;
            }
            return priceOfOne * randomOrder.Item2;
        }

    }
}
