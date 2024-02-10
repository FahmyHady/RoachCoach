using Entitas.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoachCoach
{
    public class GameSystems : Feature
    {
        public GameSystems(GameContext gameContext, IGameConfig gameConfig)
        {
            // Add(new TutorialSystems(gameContext));
            Add(new VisualsCreationSystem(gameContext));
            Add(new GameIntializationSystem(gameContext, gameConfig));
            // Add(gameContext.CreateEventSystems());
            //Add(gameContext.CreateCleanupSystems());
        }

    }
}
