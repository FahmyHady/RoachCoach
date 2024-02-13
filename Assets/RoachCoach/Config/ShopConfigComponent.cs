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
       // public (Vector3, Quaternion)[] GetCustomerCreationSpots();
        public (Vector3, Quaternion)[] GetChefCreationSpots();
        public MachineCreationEntityData[] GetMachineCreationData();
        public (CommodityType, int) GetOrderData();
        int TacoPrice { get; }
        int SodaPrice { get; }

        int MaxChefNumber { get; }
        int CurrentChefNumber { get; }
        int MaxCustomerCount { get; }
        int CurrentCustomerCount { get; }
        float ChefMovementSpeed { get; }
        float ChefOrderTakingDuration { get; }
        float CustomerMovementSpeed { get; }
    }
}
