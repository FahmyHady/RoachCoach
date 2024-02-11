using Entitas;
using System.Collections.Generic;
using static RoachCoach.RoachCoachGameCustomerMatcher;
using static RoachCoach.RoachCoachGameFreeMatcher;
using static RoachCoach.RoachCoachGameOrderMatcher;
using static RoachCoach.RoachCoachGameChefMatcher;
using static RoachCoach.RoachCoachGameCharacterMatcher;
using UnityEngine;
namespace RoachCoach
{
    public class MoveToTakeOrderSystem : IExecuteSystem
    {
        private readonly GameContext gameContext;
        private readonly ConfigContext configContext;
        private readonly IShopConfig shopConfig;

        public MoveToTakeOrderSystem(GameContext gameContext, ConfigContext configContext) : base()
        {
            this.gameContext = gameContext;
            this.configContext = configContext;
            this.shopConfig = configContext.GetShopConfig().Value;
        }

        public void Execute()
        {
            var customersWaitingToPlaceOrder = gameContext.GetEntities(Game.Matcher.AllOf(Free, Order, Customer, Character));
            if (customersWaitingToPlaceOrder.Length == 0) return;
            var freeChefs = gameContext.GetEntities(Game.Matcher.AllOf(Free, Chef, Character));
            int count = Mathf.Min(customersWaitingToPlaceOrder.Length, freeChefs.Length);
            if (count == 0) return;

            for (int i = 0; i < count; i++)
            {
                var customer = customersWaitingToPlaceOrder[i];
                var chef = freeChefs[i];
                customer.RemoveFree();
                chef.RemoveFree();

                var customerSpot = gameContext.GetSpotAtLocation(customer.GetTransform().position);
                var chefRelatedSpot = customerSpot.GetRelatedSpot().RelatedSpot;
                chef.AddTargetLocation(chefRelatedSpot.GetTransform().position);
                chef.AddMovingToTakeAnOrder();
            }

        }

    }
}
