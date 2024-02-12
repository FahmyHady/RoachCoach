using Entitas;
using RoachCoach.Game;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using UnityEngine;
using static RoachCoach.RoachCoachGameCustomerMatcher;
using static RoachCoach.RoachCoachGameVisualReferenceMatcher;
using static UnityEditor.Progress;
using static UnityEngine.EventSystems.EventTrigger;
namespace RoachCoach
{
    //This intializes Customer spots, then any related spots to further created Customers
    public class CustomerIntializationSystem : IInitializeSystem
    {
        readonly GameContext gameContext;
        private readonly ConfigContext configContext;

        public CustomerIntializationSystem(GameContext gameContext, ConfigContext configContext) : base()
        {
            this.gameContext = gameContext;
            this.configContext = configContext;
        }

        public void Initialize()
        {
            var customerCreationSpotsData = configContext.GetShopConfig().Value.GetCustomerCreationSpots();
            for (int i = 0; i < customerCreationSpotsData.Length; i++)
            {
                var item = customerCreationSpotsData[i];
                CreateCustomerSpot(item.Item1, item.Item2, i + 1);
            }
        }

        Game.Entity CreateCustomerSpot(Vector3 posOfCustomer, Quaternion rotationOfCustomer, int id)
        {
            var CustomerSpot = gameContext.CreateEntity()
       .AddFree()
       .AddCustomer()
       .AddSpot()
       .AddId(id)
       .AddTransform(posOfCustomer, rotationOfCustomer);//Should add visual representation of creator and wait for player click
                                                        // .AddVisualRepresentation(VisualType.Customer);
            return CustomerSpot;
        }

    }
}
