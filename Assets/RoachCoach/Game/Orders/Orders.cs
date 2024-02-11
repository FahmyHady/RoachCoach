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
        //public CommodityType commodityType;
        //public int amount;
    }
}
