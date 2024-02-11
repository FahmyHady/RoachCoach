using Entitas;
using System.Collections.Generic;
using static RoachCoach.RoachCoachGameChefMatcher;
using static RoachCoach.RoachCoachGameDelayMatcher;
using static RoachCoach.RoachCoachGameOrderMatcher;
using static RoachCoach.RoachCoachGameCharacterMatcher;
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
            foreach (var entity in entities)
            {
                entity.RemoveOrder();
                gameContext.CreateOrder(entity.GetTransform().position);
                entity.AddFree();
            }
        }

        protected override bool Filter(Game.Entity entity)
        {
            return !entity.HasDelay() && entity.HasOrder();
        }

        protected override ICollector<Game.Entity> GetTrigger(IContext<Game.Entity> context)
        {
            return context.CreateCollector(Game.Matcher.AllOf(Chef, Character, Order).NoneOf(Delay));
        }


    }
}
