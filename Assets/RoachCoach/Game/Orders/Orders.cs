using Entitas;
using Entitas.Generators.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoachCoach
{
    [Context(typeof(GameContext))]
    public sealed class MovingToOrderSomethingComponent : IComponent
    {
    }
    [Context(typeof(GameContext))]
    public sealed class MovingToTakeAnOrderComponent : IComponent
    {
    }
    [Context(typeof(GameContext))]
    public sealed class MovingToMakeAnOrderComponent : IComponent
    {
    }
    [Context(typeof(GameContext))]
    public sealed class OrderComponent : IComponent
    {
    }
    [Context(typeof(GameContext))]
    public sealed class TakingAnOrderComponent : IComponent
    {
    }
    [Context(typeof(GameContext))]
    public sealed class WaitingToPlaceOrderComponent : IComponent
    {
    }
    [Context(typeof(GameContext))]
    public sealed class RelatedCustomerComponent : IComponent
    {
        public Game.Entity RelatedCustomer;
    }
    [Context(typeof(GameContext))]
    public sealed class RelatedChefsCountComponent : IComponent
    {
        public int RelatedChefsCount;
    }
    [Context(typeof(GameContext))]
    public sealed class RelatedOrderComponent : IComponent
    {
        public Game.Entity RelatedOrder;
    }
    [Context(typeof(GameContext))]
    public sealed class RelatedMachineComponent : IComponent
    {
        public Game.Entity RelatedMachine;
    }

}
