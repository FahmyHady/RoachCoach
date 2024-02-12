using Entitas;
using Entitas.Generators.Attributes;
using UnityEngine;

namespace RoachCoach
{
    [Context(typeof(GameContext))]
    public sealed class IdComponent : IComponent
    {
        public int Value;
    }
}
