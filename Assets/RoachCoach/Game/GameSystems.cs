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
            Add(new VisualsCreationSystem(gameContext));
            Add(new GameIntializationSystem(gameContext, configContext.GetGameConfig().Value));
            Add(new OutletIntializationSystem(gameContext));
            Add(new ChefBrainSystem(gameContext));


            // Events (Generated)
            Add(configContext.CreateEventSystems());
            Add(inputContext.CreateEventSystems());
            Add(gameContext.CreateEventSystems());
            // Add(gameContext.CreateEventSystems());
            //Add(gameContext.CreateCleanupSystems());
        }

    }
}
