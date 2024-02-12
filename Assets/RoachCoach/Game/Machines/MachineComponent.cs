using Entitas;
using Entitas.Generators.Attributes;


namespace RoachCoach
{
    //every machine created will add to the possible order types for customers, that's why we need config context
    [Context(typeof(GameContext)), Event(EventTarget.Any)]
    public sealed class MachineComponent : IComponent
    {
    }
}
