using Entitas.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoachCoach
{
    public class CoreLoopSystems : Feature
    {
        public CoreLoopSystems(GameContext gameContext, ConfigContext configContext)
        {
            //List of systems that makeup the game loop
            //I do hope this is the right way to use Entitas
            //An alternative would have been to use DelayAction and add it to anything with delay
            //Which would allow me to cut these systems in half basically
            //However, passing an action around (managed reference) seems to be the wrong idea with entitas
            Add(new CustomerWaitingToPlaceOrderSystem(gameContext, configContext));
            Add(new ChefMoveToTakeOrderSystem(gameContext, configContext));
            Add(new ChefTakingTheOrderSystem(gameContext, configContext));
            Add(new CreateOrderSystem(gameContext, configContext));
            Add(new FreeChefsPickupFreeSodaOrdersAndGoToMachineSystem(gameContext, configContext));
            Add(new FreeChefsPickupFreeTacoOrdersAndGoToMachineSystem(gameContext, configContext));
            Add(new WaitingForMachineToFinishCookingSystem(gameContext, configContext));
            Add(new CreateCookedCommoditySystem(gameContext, configContext));
            Add(new ChefMovingBackToCustomerSystem(gameContext, configContext));
            Add(new CustomerRecieveOrderAndPaySystem(gameContext, configContext));
            Add(new CustomerLeavingSystem(gameContext, configContext));
        }
    }
}
