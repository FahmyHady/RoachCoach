using Entitas;
using System.Collections.Generic;
using static RoachCoach.RoachCoachGameChefMatcher;
using static RoachCoach.RoachCoachGameTargetLocationMatcher;
using static RoachCoach.RoachCoachGameMovingToDeliverAnOrderMatcher;
using static RoachCoach.RoachCoachGameCharacterMatcher;
using UnityEngine;
namespace RoachCoach
{
    public class ChefGivingOrderToCustomerAndGettingPaidSystem : ReactiveSystem<Game.Entity>
    {
        private readonly GameContext gameContext;
        private readonly ConfigContext configContext;

        public ChefGivingOrderToCustomerAndGettingPaidSystem(GameContext gameContext, ConfigContext configContext) : base(gameContext)
        {
            this.gameContext = gameContext;
            this.configContext = configContext;
        }
        protected override void Execute(List<Game.Entity> entities)
        {
            foreach (var chef in entities)
            {
                chef.RemoveMovingToDeliverAnOrder();
                var order = chef.GetRelatedOrder().RelatedOrder;
                var customer = order.GetRelatedCustomer().RelatedCustomer;
                chef.TransferCommodities(customer);
                chef.RemoveRelatedOrder();
                chef.AddFree();
                order.ReplaceRelatedChefsCount(order.GetRelatedChefsCount().Value - 1);
                if (DoIHaveEnoughToFulfillOrder(customer, order))
                {
                    order.AddFulfilled();
                    customer.AddTargetLocation(gameContext.GetRandomLeavingLocation());
                    customer.AddDelay(5);
                    customer.AddDelayedDestroy();
                    customer.GetRelatedSpot().RelatedSpot.AddFree();
                }
                else
                {
                    //If I don't have enough, I just wait until I do
                }
            }
        }

        protected override bool Filter(Game.Entity entity)
        {
            return !entity.HasTargetLocation() && entity.HasMovingToDeliverAnOrder();
        }

        protected override ICollector<Game.Entity> GetTrigger(IContext<Game.Entity> context)
        {
            return context.CreateCollector(Game.Matcher.AllOf(Chef, Character, MovingToDeliverAnOrder).NoneOf(TargetLocation));
        }

        public bool DoIHaveEnoughToFulfillOrder(Game.Entity customer, Game.Entity order)
        {
            var commodityOnCustomer = gameContext.GetCommodityTypeAndValueRelatedToEntity(customer);
            var commodityOnOrder = gameContext.GetCommodityTypeAndValueRelatedToEntity(order);
            if (commodityOnCustomer.type != commodityOnOrder.type) { Debug.LogError("This order does not belong to this customer"); return false; }
            if (commodityOnOrder.value <= commodityOnCustomer.value) return true;
            return false;
        }

    }
}
