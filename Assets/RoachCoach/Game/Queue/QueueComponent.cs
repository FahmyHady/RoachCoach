using Entitas;
using Entitas.Generators.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoachCoach
{
    [Context(typeof(GameContext))]
    public sealed class QueueComponent : IComponent
    {
        public IQueue queue;
    }
}
