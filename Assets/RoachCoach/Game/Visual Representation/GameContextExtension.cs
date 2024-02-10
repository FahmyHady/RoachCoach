using Entitas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace RoachCoach
{
    public static class GameContextExtension
    {
        public static Game.Entity CreateSpot(this GameContext context, Vector3 pos, Quaternion rot = default, IComponent[] spotFlags = null)
        {

            var spot = context.CreateEntity()
                    .AddSpot()
                    .AddFree()
                    .AddTransform(pos, rot);
            if (spotFlags != null)
            {
                //int lastIndex = spot.GetComponentIndexes()[^1]+1;
                //foreach (var flag in spotFlags)
                //{
                //    spot.AddComponent(lastIndex++, flag);
                //}
            }
            return spot;
        }
        public static Game.Entity CreateOutlet(this GameContext context)
        {
            var gameConfig = ConfigContext.Instance.GetGameConfig().Value;
            var transform = gameConfig.GetOutletTransform();
            return context.CreateEntity()
                .AddOutlet()
                .AddTransform(transform.Item1, transform.Item2)
                .AddVisualRepresentation(VisualType.Outlet);
        }
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
        public static Game.Entity CreateTacoMachine(this GameContext context)
        {
            var gameConfig = ConfigContext.Instance.GetGameConfig().Value;
            var chefTransform = gameConfig.GetNextChefTransform();
            return context.CreateEntity()
                .AddChef()
                .AddTransform(chefTransform.Item1, chefTransform.Item2)
                .AddMotor(gameConfig.DefaultChefSpeed)
                .AddVisualRepresentation(VisualType.TacoMachine);
        }
        public static Game.Entity CreateTacoMachineStand(this GameContext context)
        {
            var gameConfig = ConfigContext.Instance.GetGameConfig().Value;
            var machineStandTransform = gameConfig.GetNextMachineStandTransform();
            return context.CreateEntity()

                .AddTransform(machineStandTransform.Item1, machineStandTransform.Item2)
                .AddQueue(new MachineStandQueue())
                .AddVisualRepresentation(VisualType.TacoMachineStand);
        }
    }
}
