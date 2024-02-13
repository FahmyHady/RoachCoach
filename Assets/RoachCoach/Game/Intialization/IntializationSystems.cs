using Entitas.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoachCoach
{
    public class IntializationSystems : Feature
    {
        public IntializationSystems(GameContext gameContext, ConfigContext configContext)
        {
            //Intializers
            Add(new OutletIntializationSystem(gameContext, configContext));
            Add(new ChefIntializationSystem(gameContext, configContext));
            Add(new MachineIntializationSystem(gameContext, configContext));

        }
    }
}
