using Entitas;
using System.Collections.Generic;
using static RoachCoach.RoachCoachGameCustomerMatcher;
using static RoachCoach.RoachCoachGameTargetLocationMatcher;
using static RoachCoach.RoachCoachGameMovingToOrderSomethingMatcher;
using static RoachCoach.RoachCoachGameCharacterMatcher;
namespace RoachCoach
{
    public class CustomerWaitingToPlaceOrderSystem : ReactiveSystem<Game.Entity>
    {
        private readonly GameContext gameContext;
        private readonly ConfigContext configContext;
        private readonly IShopConfig shopConfig;

        public CustomerWaitingToPlaceOrderSystem(GameContext gameContext, ConfigContext configContext) : base(gameContext)
        {
            this.gameContext = gameContext;
            this.configContext = configContext;
            this.shopConfig = configContext.GetShopConfig().Value;
        }
        protected override void Execute(List<Game.Entity> entities)
        {
            foreach (var entity in entities)
            {
                entity.RemoveMovingToOrderSomething();
                entity.AddWaitingToPlaceOrder();
                entity.AddFree();
                //Idle customer waiting for chef to take his order
            }
        }

        protected override bool Filter(Game.Entity entity)
        {
            return entity.HasMovingToOrderSomething() && !entity.HasTargetLocation();
        }

        protected override ICollector<Game.Entity> GetTrigger(IContext<Game.Entity> context)
        {
            return context.CreateCollector(Game.Matcher.AllOf(Customer, Character, MovingToOrderSomething).NoneOf(TargetLocation));
        }


    }
}
