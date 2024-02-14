using Entitas;
using Entitas.Generators.Attributes;
using UnityEngine;

namespace RoachCoach
{
    [Context(typeof(GameContext)), Cleanup(CleanupMode.RemoveComponent)]
    public sealed class CreateComponent : IComponent
    {
    }
}
