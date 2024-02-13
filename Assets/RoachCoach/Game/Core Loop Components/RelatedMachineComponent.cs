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
    public sealed class RelatedMachineComponent : IComponent
    {
        public Game.Entity RelatedMachine;
    }

}
