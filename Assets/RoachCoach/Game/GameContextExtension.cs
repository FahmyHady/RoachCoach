using Entitas;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using static RoachCoach.RoachCoachGameSpotMatcher;
namespace RoachCoach
{
    public static class GameContextExtension
    {
        public static Game.Entity GetSpotAtLocation(this GameContext context, Vector3 pos, Quaternion rot = default)
        {
            var spots = context.GetEntities(Game.Matcher.AnyOf(Spot));
            Game.Entity bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            foreach (Game.Entity spot in spots)
            {
                Vector3 vectorToTarget = spot.GetTransform().position - pos;
                float magnitude = vectorToTarget.sqrMagnitude;
                if (magnitude < closestDistanceSqr)
                {
                    closestDistanceSqr = magnitude;
                    bestTarget = spot;
                }
            }
            return bestTarget;
        }
        public static Game.Entity CreateSpot(this GameContext context, Vector3 pos, Quaternion rot = default)
        {
            var spot = context.CreateEntity()
                    .AddSpot()
                    .AddFree()
                    .AddTransform(pos, rot);
            return spot;
        }

        public static Game.Entity CreateOutlet(this GameContext context)
        {
            var gameConfig = ConfigContext.Instance.GetShopConfig().Value;
            var transform = gameConfig.GetOutletTransform();
            return context.CreateEntity()
                .AddOutlet()
                .AddTransform(transform.Item1, transform.Item2)
                .AddVisualRepresentation(VisualType.Outlet);
        }


   
    }
}
