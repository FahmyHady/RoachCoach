using Entitas;
using Entitas.Generators.Attributes;
using UnityEngine;


namespace RoachCoach
{
    [Context(typeof(GameContext))]
    public sealed class VisualReferenceComponent : IComponent
    {
        public IVisual visualInterface;
    }
}
