using Entitas;
using System.Collections.Generic;
using static RoachCoach.RoachCoachGameChefMatcher;
using static RoachCoach.RoachCoachGameTargetLocationMatcher;
using static RoachCoach.RoachCoachGameMovingToTakeAnOrderMatcher;
using static RoachCoach.RoachCoachGameCharacterMatcher;
namespace RoachCoach
{
    public class ChefTakingTheOrderSystem : ReactiveSystem<Game.Entity>
    {
        private readonly GameContext gameContext;
        private readonly ConfigContext configContext;
        private readonly IShopConfig shopConfig;

        public ChefTakingTheOrderSystem(GameContext gameContext, ConfigContext configContext) : base(gameContext)
        {
            this.gameContext = gameContext;
            this.configContext = configContext;
            this.shopConfig = configContext.GetShopConfig().Value;
        }
        protected override void Execute(List<Game.Entity> entities)
        {
            foreach (var entity in entities)
            {
                entity.RemoveMovingToTakeAnOrder();
                entity.AddDelay(shopConfig.ChefOrderTakingDuration);
                entity.AddTakingAnOrder();//chef is taking order
            }
        }

        protected override bool Filter(Game.Entity entity)
        {
            return !entity.HasTargetLocation() && entity.HasMovingToTakeAnOrder();
        }

        protected override ICollector<Game.Entity> GetTrigger(IContext<Game.Entity> context)
        {
            return context.CreateCollector(Game.Matcher.AllOf(Chef, Character, MovingToTakeAnOrder).NoneOf(TargetLocation));
        }


    }
}
