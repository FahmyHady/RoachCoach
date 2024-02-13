using Entitas;
using Entitas.Generators.Attributes;

namespace RoachCoach
{
    [Context(typeof(GameContext))]
    public sealed class RelatedOrderComponent : IComponent
    {
        public Game.Entity RelatedOrder;
    }

}
