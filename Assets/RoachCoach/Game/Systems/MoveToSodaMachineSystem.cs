using Entitas;
using System.Collections.Generic;
using static RoachCoach.RoachCoachGameCharacterMatcher;
using static RoachCoach.RoachCoachGameFreeMatcher;
using static RoachCoach.RoachCoachGameOrderMatcher;
using static RoachCoach.RoachCoachGameChefMatcher;
using static RoachCoach.RoachCoachGameSodaMatcher;
using static RoachCoach.RoachCoachGameMachineMatcher;
using UnityEngine;
namespace RoachCoach
{
    public class MoveToSodaMachineSystem : IExecuteSystem
    {
        private readonly GameContext gameContext;
        private readonly ConfigContext configContext;
        private readonly IShopConfig shopConfig;

        public MoveToSodaMachineSystem(GameContext gameContext, ConfigContext configContext) : base()
        {
            this.gameContext = gameContext;
            this.configContext = configContext;
            this.shopConfig = configContext.GetShopConfig().Value;
        }

        public void Execute()
        {
            var openSodaMachines = gameContext.GetEntities(Game.Matcher.AllOf(Free, Machine, Soda));
            if (openSodaMachines.Length == 0) return;
            var openOrders = gameContext.GetEntities(Game.Matcher.AllOf(Free, Order, Soda));
            if (openOrders.Length == 0) return;
            var freeChefs = gameContext.GetEntities(Game.Matcher.AllOf(Free, Chef, Character));
            int count = Mathf.Min(openOrders.Length, freeChefs.Length, openSodaMachines.Length);
            if (count == 0) return;

            for (int i = 0; i < count; i++)
            {
                var machine = openSodaMachines[i];
                var order = openOrders[i];
                var chef = freeChefs[i];
                order.RemoveFree();
                chef.RemoveFree();
                machine.RemoveFree();

                chef.AddTargetLocation(machine.GetTransform().position);
                chef.AddMovingToMakeAnOrder();
                chef.AddSoda(1);
            }

        }
        //     public void Execute()
        //{
        //    var openSodaMachines = gameContext.GetEntities(Game.Matcher.AllOf(Free, Machine, Soda));
        //    if (openSodaMachines.Length == 0) return;
        //    var openOrders = gameContext.GetEntities(Game.Matcher.AllOf(Free, Order, Soda));
        //    if (openOrders.Length == 0) return;
        //    var freeChefs = gameContext.GetEntities(Game.Matcher.AllOf(Free, Chef, Character));
        //    int count = Mathf.Min(openOrders.Length, freeChefs.Length, openSodaMachines.Length);
        //    if (count == 0) return;

        //    for (int i = 0; i < count; i++)
        //    {
        //        var machine = openSodaMachines[i];
        //        var order = openOrders[i];
        //        var chef = freeChefs[i];
        //        order.RemoveFree();
        //        chef.RemoveFree();
        //        machine.RemoveFree();

        //        chef.AddTargetLocation(machine.GetTransform().position);
        //        chef.AddMovingToMakeAnOrder();
        //        chef.AddSoda(1);
        //    }

        //}

    }
}
