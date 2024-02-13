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

        public static (CommodityType type, int value) GetCommodityTypeAndValueRelatedToEntity(this GameContext context, Game.Entity entity)
        {
            if (entity.HasSoda())
                return (CommodityType.Soda, entity.GetSoda().Value);

            if (entity.HasTaco())
                return (CommodityType.Taco, entity.GetTaco().Value);
            return default;
        }
        public static Game.Entity AddCommodity(this Game.Entity entity, (CommodityType type, int value) toAdd, bool addVisualRepresentation = false)
        {
            switch (toAdd.type)
            {
                case CommodityType.Taco:
                    entity.AddTaco(toAdd.value);
                    break;
                case CommodityType.Soda:
                    entity.AddSoda(toAdd.value);
                    break;
            }
            if (addVisualRepresentation)
                entity.AddVisualRepresentation(VisualType.Commodity);
            return entity;
        }

        public static void TransferCommodities(this Game.Entity from, Game.Entity to)
        {
            if (from.HasSoda())
            {
                int totalSodaValue = from.GetSoda().Value + (to.HasSoda() ? to.GetSoda().Value : 0);
                to.ReplaceSoda(totalSodaValue);
                from.RemoveSoda();
            }
            if (from.HasTaco())
            {
                int totalTacoValue = from.GetTaco().Value + (to.HasTaco() ? to.GetTaco().Value : 0);
                to.ReplaceTaco(totalTacoValue);
                from.RemoveTaco();
            }
        }

        public static Vector3 GetRandomLeavingLocation(this GameContext context)
        {
            Vector3 projectedPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, Random.Range(0.35f, 1.5f), Camera.main.nearClipPlane));
            Vector3 toUse = new Vector3(projectedPoint.x, 0, projectedPoint.y);
            return toUse;
        }
        public static Vector3 GetRandomEntryLocation(this GameContext context)
        {
            Vector3 enterFromLeftOrTop = Vector3.zero;
            if (Random.value > 0.5f)
            {
                enterFromLeftOrTop = new Vector3(1.1f, Random.Range(0.35f, 1.5f),Camera.main.nearClipPlane);
            }
            else
            {
                enterFromLeftOrTop = new Vector3(Random.Range(0, 1), 1.1f, Camera.main.nearClipPlane);
            }
            Vector3 projectedPoint = Camera.main.ViewportToWorldPoint(enterFromLeftOrTop);
            Vector3 toUse = new Vector3(projectedPoint.x, 0, projectedPoint.y);
            return toUse;
        }
    }
}
