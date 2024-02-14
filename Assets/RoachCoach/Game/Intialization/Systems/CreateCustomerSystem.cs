using Entitas;
using System.Collections.Generic;
using static RoachCoach.RoachCoachGameCustomerMatcher;
using static RoachCoach.RoachCoachGameSpotMatcher;
using static RoachCoach.RoachCoachGameOutletMatcher;
using static RoachCoach.RoachCoachGameCharacterMatcher;
using static RoachCoach.RoachCoachGameFreeMatcher;
using UnityEngine;
using System.Linq;
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
            int maxCustomerCount = configContext.GetShopConfig().Value.CurrentCustomerCount;
            int currentCustomerCount = gameContext.GetEntities(Game.Matcher.AllOf(Customer, Character)).Length;
            int difference = maxCustomerCount - currentCustomerCount;
            if (difference <= 0) return;
            var freeOutletSpots = gameContext.GetEntities(Game.Matcher.AllOf(Free, Outlet, Customer, Spot)).ToList();
            if (freeOutletSpots.Count == 0) return;

            int countToCreate = Mathf.Min(difference, freeOutletSpots.Count);

            for (int i = 0; i < countToCreate; i++)
            {
                var randomSpot = freeOutletSpots.RandomElement();
                CreateCustomer(randomSpot);
                freeOutletSpots.Remove(randomSpot);
            }

        }

        Game.Entity CreateCustomer(Game.Entity targetSpot)
        {
            var targetTransform = targetSpot.GetTransform();
            targetSpot.RemoveFree();
            var creationSpot = gameContext.GetRandomEntryLocation();
            return gameContext.CreateEntity()
            .AddCharacter()
            .AddCustomer()
            .AddTransform(creationSpot, default)
                .AddMotor(configContext.GetShopConfig().Value.CustomerMovementSpeed)
                .AddVisualRepresentation(VisualType.Customer)
                .AddMovingToOrderSomething()
                .AddTargetLocation(targetTransform.position, targetTransform.rotation)
                .AddRelatedSpot(targetSpot); //Customer Spot

        }
    }
}
