using Entitas.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoachCoach
{
    internal class IntializationSystems : Feature
    {
        public IntializationSystems(GameContext gameContext, ConfigContext configContext)
        {
            //Intializers
            Add(new OutletIntializationSystem(gameContext));
            Add(new CustomersIntializationSystem(gameContext, configContext));
            Add(new ChefsIntializationSystem(gameContext, configContext));
            Add(new MachinesIntializationSystem(gameContext, configContext));

            //Creators
            Add(new CreateOutletSystem(gameContext,configContext));
            Add(new CreateMachineSystem(gameContext,configContext));
            Add(new CreateChefSystem(gameContext,configContext));
            Add(new CreateCustomerSystem(gameContext, configContext));
            Add(new CreateOrderSystem(gameContext, configContext));
        }
    }
}
