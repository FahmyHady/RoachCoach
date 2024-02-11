using Entitas;
using System.Collections.Generic;
using static RoachCoach.RoachCoachGameCustomerMatcher;
using static RoachCoach.RoachCoachGameSpotMatcher;
using static RoachCoach.RoachCoachGameOutletMatcher;
using static RoachCoach.RoachCoachGameFreeMatcher;
using UnityEngine;
namespace RoachCoach
{
    public class CreateCustomerSystem : ReactiveSystem<Game.Entity>
    {
        private readonly GameContext gameContext;
        private readonly ConfigContext configContext;

        public CreateCustomerSystem(GameContext gameContext, ConfigContext configContext) : base(gameContext)
        {
            this.gameContext = gameContext;
            this.configContext = configContext;
        }
        protected override void Execute(List<Game.Entity> entities)
        {
            int maxCustomerCount = configContext.GetShopConfig().Value.MaxCustomerCount;
            int currentCustomerCount = gameContext.GetEntities(Game.Matcher.AnyOf(Customer).NoneOf(Spot)).Length;
            int difference = maxCustomerCount - currentCustomerCount;
            for (int i = 0; i < difference; i++)
            {
                if (i < entities.Count)
                    CreateCustomer(entities[i].GetTransform().position);
            }
        }

        protected override bool Filter(Game.Entity entity)
        {
            return entity.HasFree();
        }

        protected override ICollector<Game.Entity> GetTrigger(IContext<Game.Entity> context)
        {
            return context.CreateCollector(Game.Matcher.AllOf(Free, Outlet, Customer, Spot));
        }

        Game.Entity CreateCustomer(Vector3 targetLocation)
        {
            var gameConfig = configContext.GetShopConfig().Value;
            var transform = gameConfig.GetNextCustomerTransform();
            return gameContext.CreateEntity()
            .AddCharacter()
            .AddCustomer()
            .AddTransform(transform.Item1, transform.Item2)
                .AddMotor(gameConfig.CustomerMovementSpeed)
                .AddVisualRepresentation(VisualType.Customer)
                .AddMovingToOrderSomething()
                .AddTargetLocation(targetLocation);
        }
    }
}
