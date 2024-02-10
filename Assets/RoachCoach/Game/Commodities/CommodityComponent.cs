using Entitas;
using Entitas.Generators.Attributes;


namespace RoachCoach
{
    [Context(typeof(GameContext))]
    public sealed class CommodityComponent : IComponent
    {
        public CommodityType Type;
    }
}
