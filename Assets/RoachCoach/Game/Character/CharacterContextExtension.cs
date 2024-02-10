using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace RoachCoach
{
    public static class CharacterContextExtension
    {
        public static Game.Entity CreateRandomChef(this GameContext context)
        {
            var gameConfig = ConfigContext.Instance.GetGameConfig().Value;
            var chefTransform = gameConfig.GetNextChefTransform();
            return context.CreateEntity()
                .AddChef()
                .AddTransform(chefTransform.Item1, chefTransform.Item2)
                .AddMotor(gameConfig.DefaultChefSpeed)
                .AddVisualRepresentation(VisualType.Chef);
        }
    }
}
