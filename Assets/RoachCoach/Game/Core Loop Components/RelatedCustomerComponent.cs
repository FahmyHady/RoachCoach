using Entitas;
using Entitas.Generators.Attributes;

namespace RoachCoach
{
    [Context(typeof(GameContext))]
    public sealed class RelatedCustomerComponent : IComponent
    {
        public Game.Entity RelatedCustomer;
    }

}
