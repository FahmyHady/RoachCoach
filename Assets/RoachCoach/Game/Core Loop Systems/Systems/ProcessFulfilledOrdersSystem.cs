using Entitas;
using static RoachCoach.RoachCoachGameOrderMatcher;
using static RoachCoach.RoachCoachGameFulfilledMatcher;
using System.Collections.Generic;
namespace RoachCoach
{
    internal class ProcessFulfilledOrdersSystem : ReactiveSystem<Game.Entity>
    {
        private readonly GameContext gameContext;

        public ProcessFulfilledOrdersSystem(GameContext gameContext) : base(gameContext)
        {
            this.gameContext = gameContext;
        }

        protected override void Execute(List<Game.Entity> entities)
        {
            foreach (var order in entities)
            {
                gameContext.ReplaceWallet(order.GetMoney().Value + gameContext.GetWallet().Value);
                order.RemoveFulfilled();
                order.AddDestroyed();
                //GetMoney
            }
        }

        protected override bool Filter(Game.Entity entity)
        {
            return entity.HasOrder() && entity.HasFulfilled();
        }

        protected override ICollector<Game.Entity> GetTrigger(IContext<Game.Entity> context)
        {
            return context.CreateCollector(Game.Matcher.AllOf(Order, Fulfilled));
        }
    }
}
