using Entitas;
using RoachCoach.Game;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using UnityEngine;
using static RoachCoach.RoachCoachGameChefMatcher;
using static RoachCoach.RoachCoachGameVisualReferenceMatcher;
using static UnityEditor.Progress;
using static UnityEngine.EventSystems.EventTrigger;
namespace RoachCoach
{
    //This intializes Chef spots, then any related spots to further created Chefs
    public class ChefIntializationSystem : IInitializeSystem
    {
        readonly GameContext gameContext;
        private readonly ConfigContext configContext;

        public ChefIntializationSystem(GameContext gameContext, ConfigContext configContext) : base()
        {
            this.gameContext = gameContext;
            this.configContext = configContext;
        }

        public void Initialize()
        {
            var ChefCreationSpotsData = configContext.GetShopConfig().Value.GetChefCreationSpots();
            for (int i = 0; i < ChefCreationSpotsData.Length; i++)
            {
                var item = ChefCreationSpotsData[i];
                var chefSpot = CreateChefSpot(item.Item1, item.Item2, i + 1);
                if (i == 0) chefSpot.AddCreate();//create first chef
            }
        }

        Game.Entity CreateChefSpot(Vector3 posOfChef, Quaternion rotationOfChef, int id)
        {
            var ChefSpot = gameContext.CreateEntity()
       .AddFree()
       .AddChef()
       .AddSpot()
       .AddId(id)
       .AddTransform(posOfChef, rotationOfChef);//Should add visual representation of creator and wait for player click
                                                // .AddVisualRepresentation(VisualType.Chef);
            return ChefSpot;
        }

    }
}
