using Entitas;
using System.Collections.Generic;
using static RoachCoach.RoachCoachGameChefMatcher;
using static RoachCoach.RoachCoachGameDelayMatcher;
using static RoachCoach.RoachCoachGameTakingAnOrderMatcher;
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
                entity.RemoveTakingAnOrder();
                CreateOrder(entity.GetRelatedCustomer().RelatedCustomer);
                entity.RemoveRelatedCustomer();
                entity.AddFree();
            }
        }

        protected override bool Filter(Game.Entity entity)
        {
            return !entity.HasDelay() && entity.HasOrder();
        }

        protected override ICollector<Game.Entity> GetTrigger(IContext<Game.Entity> context)
        {
            return context.CreateCollector(Game.Matcher.AllOf(Chef, Character, TakingAnOrder).NoneOf(Delay));
        }
        Game.Entity CreateOrder(Game.Entity relatedCustomer)
        {
            var randomOrder = shopConfig.GetOrderData();
            var orderEntity = gameContext.CreateEntity().AddFree().AddOrder().AddRelatedCustomer(relatedCustomer);
            switch (randomOrder.Item1)
            {
                case CommodityType.Taco:
                    orderEntity.AddTaco(randomOrder.Item2).AddVisualRepresentation(VisualType.Taco);
                    break;
                case CommodityType.Soda:
                    orderEntity.AddSoda(randomOrder.Item2).AddVisualRepresentation(VisualType.Soda);
                    break;
                default:
                    break;
            }
            return orderEntity;
        }

    }
}
