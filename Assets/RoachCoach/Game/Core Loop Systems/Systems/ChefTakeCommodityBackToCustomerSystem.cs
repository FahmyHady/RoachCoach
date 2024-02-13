using Entitas;
using System.Collections.Generic;
using static RoachCoach.RoachCoachGameChefMatcher;
using static RoachCoach.RoachCoachGameDelayMatcher;
using static RoachCoach.RoachCoachGameMakingAnOrderMatcher;
using static RoachCoach.RoachCoachGameCharacterMatcher;
namespace RoachCoach
{
    public class ChefTakeCommodityBackToCustomerSystem : ReactiveSystem<Game.Entity>
    {
        private readonly GameContext gameContext;
        private readonly ConfigContext configContext;

        public ChefTakeCommodityBackToCustomerSystem(GameContext gameContext, ConfigContext configContext) : base(gameContext)
        {
            this.gameContext = gameContext;
            this.configContext = configContext;
        }
        protected override void Execute(List<Game.Entity> entities)
        {
            foreach (var chef in entities)
            {

                var machine = chef.GetRelatedMachine().RelatedMachine;
                var relatedCommodity = gameContext.GetCommodityTypeAndValueRelatedToEntity(machine);

                chef.RemoveMakingAnOrder();
                chef.AddCommodity(relatedCommodity);//chef is carrying the order
                machine.AddFree();
                chef.RemoveRelatedMachine();


                //This could've been easier but this allows us to do things like for example the customer orders from
                //A drive-through window but the chef delivers to a different place
                //All customers can have same delivery spot or in this case, each has a unique one
                var relatedCustomerSpot = chef.GetRelatedOrder().RelatedOrder.GetRelatedCustomer().RelatedCustomer.GetRelatedSpot().RelatedSpot;
                var chefDeliverySpotRelatingToCustomerSpot = relatedCustomerSpot.GetRelatedSpot().RelatedSpot; //Delivery spot
                chef.AddTargetLocation(chefDeliverySpotRelatingToCustomerSpot.GetTransform().position);
                chef.AddMovingToDeliverAnOrder();

            }
        }

        protected override bool Filter(Game.Entity entity)
        {
            return !entity.HasTargetLocation() && entity.HasMakingAnOrder();
        }

        protected override ICollector<Game.Entity> GetTrigger(IContext<Game.Entity> context)
        {
            return context.CreateCollector(Game.Matcher.AllOf(Chef, Character, MakingAnOrder).NoneOf(Delay));
        }


    }
}
