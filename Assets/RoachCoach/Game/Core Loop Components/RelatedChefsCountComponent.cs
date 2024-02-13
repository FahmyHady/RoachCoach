using Entitas;
using Entitas.Generators.Attributes;

namespace RoachCoach
{
    [Context(typeof(GameContext))]
    public sealed class RelatedChefsCountComponent : IComponent
    {
        public int Value;
    }

}
