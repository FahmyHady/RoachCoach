using Entitas;
using Entitas.Generators.Attributes;


namespace RoachCoach
{
    [Context(typeof(GameContext))]
    public sealed class VisualRepresentationComponent : IComponent
    {
        public VisualType Type;
    }
}
