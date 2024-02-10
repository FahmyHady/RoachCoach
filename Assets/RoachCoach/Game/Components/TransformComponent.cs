using Entitas;
using Entitas.Generators.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoachCoach
{
    [Context(typeof(GameContext)), Event(EventTarget.Self)]
    public sealed class TransformComponent : IComponent
    {
        public Vector3 position;
        public Quaternion rotation;
    }
}
