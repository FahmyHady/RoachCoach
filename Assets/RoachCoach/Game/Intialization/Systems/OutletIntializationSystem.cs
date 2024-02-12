using Entitas;
using RoachCoach.Game;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using UnityEngine;
using static RoachCoach.RoachCoachGameOutletMatcher;
using static RoachCoach.RoachCoachGameVisualReferenceMatcher;
using static UnityEngine.EventSystems.EventTrigger;
namespace RoachCoach
{
    //This intializes outlet spots, then any related spots to further created outlets
    public class OutletIntializationSystem : ReactiveSystem<Game.Entity>, IInitializeSystem
    {
        readonly GameContext gameContext;
        private readonly ConfigContext configContext;

        public OutletIntializationSystem(GameContext gameContext, ConfigContext configContext) : base(gameContext)
        {
            this.gameContext = gameContext;
            this.configContext = configContext;
        }

        public void Initialize()
        {
            CreateAnOuletSpot(1);
        }

        protected override void Execute(List<Game.Entity> entities)
        {
            foreach (var outlet in entities)
            {
                CreateOutletRelatedSpots(outlet);
            }
        }

        protected override bool Filter(Game.Entity entity)
        {
            return entity.HasOutlet() && entity.HasVisualReference();
        }

        protected override ICollector<Game.Entity> GetTrigger(IContext<Game.Entity> context)
        {
            return context.CreateCollector(Matcher.AllOf(Outlet, VisualReference));
        }

        void CreateAnOuletSpot(int id)
        {
            var shopConfig = configContext.GetShopConfig().Value;
            var transform = shopConfig.GetOutletTransform();
            gameContext.CreateEntity()
      .AddCreate()
      .AddFree()
      .AddOutlet()
      .AddSpot()
      .AddId(id)
      .AddTransform(transform.Item1, transform.Item2);//Should add visual representation of creator and wait for player click
                                                      // .AddVisualRepresentation(VisualType.Outlet);
        }

        void CreateOutletRelatedSpots(Game.Entity OuletEntity)
        {
            var outLetSlots = ((IOutletVisual)OuletEntity.GetVisualReference().visualInterface).GetSpotLocations();
            int id = OuletEntity.GetId().Value;
            foreach (var item in outLetSlots)
            {
                //rotation can be added later, it's supposed to define where the character faces when standing in this spot
                var relatedChefSlot = gameContext.CreateSpot(item.chefSpot, Quaternion.identity/* item.chefSpot.rotation*/).AddChef().AddOutlet().AddId(id);
                var customerSlot = gameContext.CreateSpot(item.customerSpot, Quaternion.identity/* item.customerSpot.rotation*/).AddCustomer().AddOutlet().AddRelatedSpot(relatedChefSlot).AddId(id);
            }
        }
    }
}
