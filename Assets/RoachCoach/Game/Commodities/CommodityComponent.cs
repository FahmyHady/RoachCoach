using Entitas;
using Entitas.Generators.Attributes;


namespace RoachCoach
{
    [Context(typeof(GameContext))]
    public sealed class CommodityComponent : IComponent
    {
        public CommodityType Type;
    }
    [Context(typeof(GameContext)), Event(EventTarget.Self)]
    public sealed class SodaComponent : IComponent
    {
        public int Value;
    }
    [Context(typeof(GameContext)), Event(EventTarget.Self)]
    public sealed class TacoComponent : IComponent
    {
        public int Value;
    }
}
