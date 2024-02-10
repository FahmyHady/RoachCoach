using Entitas;
using Entitas.Generators.Attributes;
using RoachCoach;
using UnityEngine;

namespace RoachCoach
{
    [Context(typeof(ConfigContext)), Unique]
    public sealed class GameConfigComponent : IComponent
    {
        public IGameConfig Value;
    }

    public interface IGameConfig
    {
        public (Vector3, Quaternion) GetNextChefTransform();
        public (Vector3, Quaternion) GetNextCustomerTransform();
        public (Vector3, Quaternion) GetNextMachineStandTransform();
        public (Vector3, Quaternion) GetNextMachineTransform();
        int StartingChefNumber { get; }
        float DefaultChefSpeed { get; }
        int StartingMachineStands { get; }
        int StartingCustomerCount { get; }
        float DefaultCustomerSpeed { get; }
    }
}
