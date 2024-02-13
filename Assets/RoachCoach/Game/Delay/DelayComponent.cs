using Entitas;
using Entitas.Generators.Attributes;
using System;

namespace RoachCoach
{
    [Context(typeof(GameContext)), Event(EventTarget.Self, EventType.Removed), Event(EventTarget.Self, EventType.Added)]
    public sealed class DelayComponent : IComponent
    {
        public float ValueInSeconds;
    }

    //This doesn't seem to fit with how Entitas is supposed to work, but I'm putting it as a last resort
    [Context(typeof(GameContext))]
    public sealed class DelayedDestroyComponent : IComponent
    {
    }
}
