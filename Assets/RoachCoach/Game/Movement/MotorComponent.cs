using Entitas;
using Entitas.Generators.Attributes;
using UnityEngine;

namespace RoachCoach
{
    [Context(typeof(GameContext))]
    public sealed class TargetLocationComponent : IComponent
    {
        public Vector3 targetPos;
    }
}
