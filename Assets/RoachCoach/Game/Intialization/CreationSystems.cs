using Entitas.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoachCoach
{
    public class CreationSystems : Feature
    {
        public CreationSystems(GameContext gameContext, ConfigContext configContext)
        {
            //Creators
            Add(new CreateOutletSystem(gameContext,configContext));
            Add(new CreateMachineSystem(gameContext,configContext));
            Add(new CreateMachineStandSystem(gameContext,configContext));
            Add(new CreateChefSystem(gameContext,configContext));
            Add(new CreateCustomerSystem(gameContext, configContext));
            Add(new CreateOrderSystem(gameContext, configContext));
            Add(new VisualsCreationSystem(gameContext, configContext));
        }
    }
}
