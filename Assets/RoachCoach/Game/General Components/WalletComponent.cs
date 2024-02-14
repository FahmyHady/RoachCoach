using Entitas;
using Entitas.Generators.Attributes;
using UnityEngine;

namespace RoachCoach
{
    [Context(typeof(GameContext)), Unique, Event(EventTarget.Any)]
    public sealed class WalletComponent : IComponent
    {
        public int Value;
    }
}
