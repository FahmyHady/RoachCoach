using Entitas;
using System.Collections.Generic;
using static RoachCoach.RoachCoachGameCustomerMatcher;
using static RoachCoach.RoachCoachGameFreeMatcher;
using static RoachCoach.RoachCoachGameWaitingToPlaceOrderMatcher;
using static RoachCoach.RoachCoachGameChefMatcher;
using static RoachCoach.RoachCoachGameCharacterMatcher;
using UnityEngine;
namespace RoachCoach
{
    public class ChefMoveToTakeOrderSystem : IExecuteSystem
    {
        private readonly GameContext gameContext;
        private readonly ConfigContext configContext;

        public ChefMoveToTakeOrderSystem(GameContext gameContext, ConfigContext configContext) : base()
        {
            this.gameContext = gameContext;
            this.configContext = configContext;
        }

        public void Execute()
        {
            var customersWaitingToPlaceOrder = gameContext.GetEntities(Game.Matcher.AllOf(Free, WaitingToPlaceOrder, Customer, Character));
            if (customersWaitingToPlaceOrder.Length == 0) return;
            var freeChefs = gameContext.GetEntities(Game.Matcher.AllOf(Free, Chef, Character));
            int count = Mathf.Min(customersWaitingToPlaceOrder.Length, freeChefs.Length);
            if (count == 0) return;

            for (int i = 0; i < count; i++)
            {
                var customer = customersWaitingToPlaceOrder[i];
                var chef = freeChefs[i];
                customer.RemoveFree();
                customer.RemoveWaitingToPlaceOrder();
                chef.RemoveFree();
                //The chef spot related to the customer spot
                var chefRelatedSpotTransform = customer.GetRelatedSpot().RelatedSpot.GetRelatedSpot().RelatedSpot.GetTransform();
                chef.AddTargetLocation(chefRelatedSpotTransform.position, chefRelatedSpotTransform.rotation);
                chef.AddMovingToTakeAnOrder();
                chef.AddRelatedCustomer(customer);
            }

        }

    }
}
