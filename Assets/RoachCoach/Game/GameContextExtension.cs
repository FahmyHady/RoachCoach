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


        public static Game.Entity CreateTacoMachine(this GameContext context)
        {
            var gameConfig = ConfigContext.Instance.GetShopConfig().Value;
            var chefTransform = gameConfig.GetNextChefTransform();
            return context.CreateEntity()
                .AddChef()
                .AddTransform(chefTransform.Item1, chefTransform.Item2)
                .AddMotor(gameConfig.ChefMovementSpeed)
                .AddVisualRepresentation(VisualType.TacoMachine);
        }
        public static Game.Entity CreateTacoMachineStand(this GameContext context)
        {
            var gameConfig = ConfigContext.Instance.GetShopConfig().Value;
            var machineStandTransform = gameConfig.GetNextMachineTransform();
            return context.CreateEntity()
                .AddTransform(machineStandTransform.Item1, machineStandTransform.Item2)
                .AddVisualRepresentation(VisualType.TacoMachineStand);
        }
        public static Game.Entity CreateOrder(this GameContext context, Vector3 pos)
        {
            var gameConfig = ConfigContext.Instance.GetShopConfig().Value;
            var randomOrder = gameConfig.GetOrderData();
            var orderEntity = context.CreateEntity().AddOrder().AddTransform(pos, Quaternion.identity).AddFree();
            switch (randomOrder.Item1)
            {
                case CommodityType.Taco:
                    orderEntity.AddTaco(randomOrder.Item2).AddVisualRepresentation(VisualType.Taco);
                    break;
                case CommodityType.Soda:
                    orderEntity.AddSoda(randomOrder.Item2).AddVisualRepresentation(VisualType.Soda);
                    break;
                default:
                    break;
            }
            return orderEntity;
        }
    }
}
