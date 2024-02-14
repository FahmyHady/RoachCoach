using Entitas;
using Entitas.Generators.Attributes;

namespace RoachCoach
{
    [Context(typeof(GameContext))]
    public sealed class MoneyComponent : IComponent
    {
        public int Value;
    }

}
