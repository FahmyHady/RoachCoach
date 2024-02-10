using Entitas;
using Entitas.Generators.Attributes;
using UnityEngine;

namespace RoachCoach
{
    [Context(typeof(GameContext)), Event(EventTarget.Self)]
    public sealed class MotorComponent : IComponent
    {
        public float speed;
    }
}
