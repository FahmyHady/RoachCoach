using Entitas;
using Entitas.Generators.Attributes;
using RoachCoach;
using System;
using UnityEngine;

namespace RoachCoach
{
    [Context(typeof(ConfigContext)), Unique]
    public sealed class ShopConfigComponent : IComponent
    {
        public IShopConfig Value;
    }

    public interface IShopConfig
    {
        public (Vector3, Quaternion) GetOutletTransform();
        public (Vector3, Quaternion) GetNextChefTransform();
        public (Vector3, Quaternion) GetNextCustomerTransform();
        public Tuple<Vector3, Quaternion>[] GetMachineStandsTransform();
        public (Vector3, Quaternion) GetNextMachineTransform();
        public (CommodityType, int) GetOrderData();
        int StartingChefNumber { get; }
        float ChefMovementSpeed { get; }
        float ChefOrderTakingDuration { get; }
        int MaxCustomerCount { get; }
        float CustomerMovementSpeed { get; }
        int StartingMachineStands { get; }
    }
}
