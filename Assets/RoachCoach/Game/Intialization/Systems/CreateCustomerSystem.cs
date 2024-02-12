using Entitas;
using System.Collections.Generic;
using static RoachCoach.RoachCoachGameCustomerMatcher;
using static RoachCoach.RoachCoachGameSpotMatcher;
using static RoachCoach.RoachCoachGameOutletMatcher;
using static RoachCoach.RoachCoachGameCharacterMatcher;
using static RoachCoach.RoachCoachGameFreeMatcher;
using UnityEngine;
namespace RoachCoach
{
    public class CreateCustomerSystem : IExecuteSystem
    {
        private readonly GameContext gameContext;
        private readonly ConfigContext configContext;

        public CreateCustomerSystem(GameContext gameContext, ConfigContext configContext) : base()
        {
            this.gameContext = gameContext;
            this.configContext = configContext;
        }

        public void Execute()
        {
            int maxCustomerCount = configContext.GetShopConfig().Value.MaxCustomerCount;
            int currentCustomerCount = gameContext.GetEntities(Game.Matcher.AllOf(Customer, Character)).Length;
            int difference = maxCustomerCount - currentCustomerCount;
            if (difference <= 0) return;
            var freeOutletSpots = gameContext.GetEntities(Game.Matcher.AllOf(Free, Outlet, Customer, Spot));
            if (freeOutletSpots.Length == 0) return;

            int countToCreate = Mathf.Min(difference, freeOutletSpots.Length);
            var creationSpots = gameContext.GetEntities(Game.Matcher.AllOf(Free, Customer, Spot).NoneOf(Outlet));

            for (int i = 0; i < countToCreate; i++)
            {
                CreateCustomer(creationSpots.RandomElement(), freeOutletSpots.RandomElement());
            }

        }

        Game.Entity CreateCustomer(Game.Entity creationSpot, Game.Entity targetSpot)
        {
            var creationTransform = creationSpot.GetTransform();
            var targetTransform = targetSpot.GetTransform();
            targetSpot.RemoveFree();

            return gameContext.CreateEntity()
            .AddCharacter()
            .AddCustomer()
            .AddTransform(creationTransform.position, creationTransform.rotation)
                .AddMotor(configContext.GetShopConfig().Value.CustomerMovementSpeed)
                .AddVisualRepresentation(VisualType.Customer)
                .AddMovingToOrderSomething()
                .AddTargetLocation(targetTransform.position)
                .AddRelatedSpot(targetSpot); //Customer Spot

        }
    }
}
