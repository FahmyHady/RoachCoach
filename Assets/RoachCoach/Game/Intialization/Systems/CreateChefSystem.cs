using Entitas;
using RoachCoach.Game;
using System.Collections.Generic;
using UnityEngine;
using static RoachCoach.RoachCoachGameChefMatcher;
using static RoachCoach.RoachCoachGameFreeMatcher;
using static RoachCoach.RoachCoachGameSpotMatcher;
using static RoachCoach.RoachCoachGameCreateMatcher;
namespace RoachCoach
{
    public class CreateChefSystem : ReactiveSystem<Game.Entity>
    {
        readonly GameContext gameContext;
        private readonly ConfigContext configContext;
        public CreateChefSystem(GameContext gameContext, ConfigContext configContext) : base(gameContext)
        {
            this.gameContext = gameContext;
            this.configContext = configContext;
        }

        protected override void Execute(List<Game.Entity> entities)
        {
            foreach (var entity in entities)
            {
                CreateChef(entity);
            }
        }

        protected override bool Filter(Game.Entity entity)
        {
            return entity.HasCreate() && entity.HasChef() && entity.HasFree() && entity.HasSpot();
        }

        protected override ICollector<Game.Entity> GetTrigger(IContext<Game.Entity> context)
        {
            return context.CreateCollector(Matcher.AllOf(Free, Chef, Spot, Create));
        }
        Game.Entity CreateChef(Game.Entity creationSpot)
        {
            var creationTransform = creationSpot.GetTransform();
            return gameContext.CreateEntity()
                .AddCharacter()
                .AddChef()
                .AddFree()
                .AddTransform(creationTransform.position, creationTransform.rotation)
                .AddMotor(configContext.GetShopConfig().Value.ChefMovementSpeed)
                .AddVisualRepresentation(VisualType.Chef);
        }
    }
}
