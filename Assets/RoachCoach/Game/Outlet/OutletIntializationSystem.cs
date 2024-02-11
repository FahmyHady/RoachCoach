using Entitas;
using RoachCoach.Game;
using System.Collections.Generic;
using static RoachCoach.RoachCoachGameOutletMatcher;
using static RoachCoach.RoachCoachGameVisualReferenceMatcher;
namespace RoachCoach
{
    public class OutletIntializationSystem : ReactiveSystem<Game.Entity>
    {
        readonly GameContext _gameContext;

        public OutletIntializationSystem(GameContext gameContext) : base(gameContext)
        {
            _gameContext = gameContext;
            gameContext.CreateOutlet();
        }

        protected override void Execute(List<Game.Entity> entities)
        {
            Dictionary<Game.Entity, Game.Entity> spotsMap = new Dictionary<Game.Entity, Game.Entity>();
            foreach (var entity in entities)
            {
                var outLetSlots = entity.GetVisualReference().visualInterface.GetConnectedObject().GetComponent<OutletMonobehaviour>().customerSpots;
                foreach (var item in outLetSlots)
                {
                    var relatedChefSlot = _gameContext.CreateSpot(item.two.position, item.two.rotation).AddChef().AddOutlet();
                    var customerSlot = _gameContext.CreateSpot(item.one.position, item.one.rotation).AddCustomer().AddOutlet().AddRelatedSpot(relatedChefSlot);
                    spotsMap.Add(customerSlot, relatedChefSlot);
                }
                //entity.ReplaceOutlet(spotsMap);
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
    }
}
