using Entitas.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoachCoach
{
    public class GameSystems : Feature
    {
        public GameSystems(GameContext gameContext, ConfigContext configContext, InputContext inputContext)
        {
            // Add(new TutorialSystems(gameContext));
            Add(new IntializationSystems(gameContext, configContext));
            Add(new VisualsCreationSystem(gameContext, configContext));
            Add(new DelaySystem(gameContext));
            Add(new MoveToTargetSystem(gameContext));
            Add(new PlaceOrderSystem(gameContext, configContext));
            Add(new MoveToTakeOrderSystem(gameContext, configContext));
            Add(new CreateOrderSystem(gameContext, configContext));
            Add(new TakeOrderFromCustomerSystem(gameContext, configContext));
            Add(new MoveToSodaMachineSystem(gameContext, configContext));


            // Events (Generated)
            Add(configContext.CreateEventSystems());
            Add(inputContext.CreateEventSystems());
            Add(gameContext.CreateEventSystems());
            // Add(gameContext.CreateEventSystems());
            //Add(gameContext.CreateCleanupSystems());
        }

    }
}
