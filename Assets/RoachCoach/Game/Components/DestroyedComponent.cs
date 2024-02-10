using Entitas;
using Entitas.Generators.Attributes;
using UnityEngine;

namespace RoachCoach
{
    [Context(typeof(GameContext)), Event(EventTarget.Self), Cleanup(CleanupMode.DestroyEntity)]
    public sealed class DestroyedComponent : IComponent
    {
    }
}
