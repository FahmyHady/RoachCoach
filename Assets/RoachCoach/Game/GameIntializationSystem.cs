using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using System.Text.RegularExpressions;
using UnityEngine.UIElements;

namespace RoachCoach
{
    public class GameIntializationSystem : IInitializeSystem
    {
        readonly GameContext gameContext;
        readonly IShopConfig gameConfig;

        public GameIntializationSystem(GameContext _gameContext, IShopConfig gameConfig) 
        {
            this.gameContext = _gameContext;
            this.gameConfig = gameConfig;
        }

        public void Initialize()
        {
            for (int i = 0; i < gameConfig.StartingChefNumber; i++)
            {
                gameContext.CreateRandomChef();
            }
        }

    
    }
}
