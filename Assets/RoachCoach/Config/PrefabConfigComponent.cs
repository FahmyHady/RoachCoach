using Entitas;
using Entitas.Generators.Attributes;
using RoachCoach;
using UnityEngine;

namespace RoachCoach
{
    [Context(typeof(ConfigContext)), Unique]
    public sealed class PrefabConfigComponent : IComponent
    {
        public IPrefabConfig Value;
    }

    public interface IPrefabConfig
    {
        public GameObject GetPrefab(VisualType type);
    }
}
