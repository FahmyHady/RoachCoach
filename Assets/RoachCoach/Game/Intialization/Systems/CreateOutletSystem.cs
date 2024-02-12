using Entitas;
using RoachCoach.Game;
using System.Collections.Generic;
using UnityEngine;
using static RoachCoach.RoachCoachGameOutletMatcher;
using static RoachCoach.RoachCoachGameFreeMatcher;
using static RoachCoach.RoachCoachGameSpotMatcher;
using static RoachCoach.RoachCoachGameCreateMatcher;
using System.Runtime.Remoting.Contexts;
namespace RoachCoach
{
    public class CreateOutletSystem : ReactiveSystem<Game.Entity>
    {
        readonly GameContext gameContext;
        private readonly ConfigContext configContext;
        public CreateOutletSystem(GameContext gameContext, ConfigContext configContext) : base(gameContext)
        {
            this.gameContext = gameContext;
            this.configContext = configContext;
        }

        protected override void Execute(List<Game.Entity> entities)
        {
            foreach (var entity in entities)
            {
                var transform = entity.GetTransform();
                entity.RemoveFree();
                gameContext.CreateEntity()
                   .AddOutlet()
                   .AddTransform(transform.position, transform.rotation)
                   .AddId(entity.GetId().Value)
                   .AddVisualRepresentation(VisualType.Outlet);
            }
        }

        protected override bool Filter(Game.Entity entity)
        {
            return entity.HasCreate() && entity.HasOutlet() && entity.HasFree() && entity.HasSpot();
        }

        protected override ICollector<Game.Entity> GetTrigger(IContext<Game.Entity> context)
        {
            return context.CreateCollector(Matcher.AllOf(Free, Outlet, Spot, Create));
        }
    }
}
