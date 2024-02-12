using Entitas;
using System.Collections.Generic;
using static RoachCoach.RoachCoachGameCharacterMatcher;
using static RoachCoach.RoachCoachGameFreeMatcher;
using static RoachCoach.RoachCoachGameOrderMatcher;
using static RoachCoach.RoachCoachGameChefMatcher;
using static RoachCoach.RoachCoachGameTacoMatcher;
using static RoachCoach.RoachCoachGameSpotMatcher;
using static RoachCoach.RoachCoachGameMachineMatcher;
using static RoachCoach.RoachCoachGameRelatedCustomerMatcher;
using UnityEngine;
using System.Linq;
using System;
namespace RoachCoach
{
    public class FreeChefsPickupFreeTacoOrdersAndGoToMachineSystem : IExecuteSystem
    {
        private readonly GameContext gameContext;
        private readonly ConfigContext configContext;
        private readonly IShopConfig shopConfig;

        public FreeChefsPickupFreeTacoOrdersAndGoToMachineSystem(GameContext gameContext, ConfigContext configContext) : base()
        {
            this.gameContext = gameContext;
            this.configContext = configContext;
            this.shopConfig = configContext.GetShopConfig().Value;
        }

        public void Execute()
        {
            var openOrders = gameContext.GetEntities(Game.Matcher.AllOf(Free, Order, Taco, RelatedCustomer));
            if (openOrders.Length == 0) return;
            var freeChefs = gameContext.GetEntities(Game.Matcher.AllOf(Free, Chef, Character)).ToList();
            if (freeChefs.Count == 0) return;
            var openTacoMachines = gameContext.GetEntities(Game.Matcher.AllOf(Free, Machine, Taco).NoneOf(Spot)).ToList();
            if (openTacoMachines.Count == 0) return;

            int maxAmountCanFulfil = Mathf.Min(openTacoMachines.Count, freeChefs.Count, openOrders.Length);
            Game.Entity chef = null;
            Game.Entity order = null;
            Game.Entity machine = null;
            int orderValue = 0;

            for (int i = 0; i < maxAmountCanFulfil; i++)
            {
                //first come, first serve
                order = openOrders[i];
                orderValue = order.GetTaco().Value;
                //Closest free chef to order, this can be optimized
                chef = freeChefs.OrderBy(a => Vector3.Distance(order.GetTransform().position, a.GetTransform().position)).First();
                //finding closest machine to chef, This can be optimized
                machine = openTacoMachines.OrderBy(a => Vector3.Distance(chef.GetTransform().position, a.GetTransform().position)).First();


                openTacoMachines.Remove(machine);
                freeChefs.Remove(chef);


                chef.RemoveFree();
                machine.RemoveFree();
                int numberOfChefsWorkingOnThisOrder = order.GetRelatedChefsCount().RelatedChefsCount + 1;
                order.AddRelatedChefsCount(numberOfChefsWorkingOnThisOrder);
                if (numberOfChefsWorkingOnThisOrder >= orderValue)
                    order.RemoveFree();

                chef.AddTargetLocation(machine.GetTransform().position);
                chef.AddMovingToMakeAnOrder();
                chef.AddRelatedOrder(order);
                chef.AddRelatedMachine(machine);
            }

        }


    }
}
