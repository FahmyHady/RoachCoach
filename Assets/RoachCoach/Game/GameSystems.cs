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
            Add(new IntializationSystems(gameContext, configContext));
            Add(new CreationSystems(gameContext, configContext));
            Add(new DelaySystem(gameContext));
            Add(new MoveToTargetSystem(gameContext));
            //Add(new PlaceOrderSystem(gameContext, configContext));
            //Add(new ChefMoveToTakeOrderSystem(gameContext, configContext));
            //Add(new CreateOrderSystem(gameContext, configContext));
            //Add(new ChefTakingTheOrderSystem(gameContext, configContext));
            //Add(new MoveToSodaMachineSystem(gameContext, configContext));


            // Events (Generated)
            Add(configContext.CreateEventSystems());
            Add(inputContext.CreateEventSystems());
            Add(gameContext.CreateEventSystems());

            //Cleanup (Generated)
            Add(configContext.CreateCleanupSystems());
            Add(inputContext.CreateCleanupSystems());
            Add(gameContext.CreateCleanupSystems());
        }

    }
}
