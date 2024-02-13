using Entitas;
using RoachCoach.Game;
using System.Collections.Generic;
using UnityEngine;
using static RoachCoach.RoachCoachGameMachineMatcher;
using static RoachCoach.RoachCoachGameFreeMatcher;
using static RoachCoach.RoachCoachGameSpotMatcher;
using static RoachCoach.RoachCoachGameCreateMatcher;
using System.Runtime.Remoting.Contexts;
namespace RoachCoach
{
    public class CreateMachineSystem : ReactiveSystem<Game.Entity>
    {
        readonly GameContext gameContext;
        private readonly ConfigContext configContext;
        public CreateMachineSystem(GameContext gameContext, ConfigContext configContext) : base(gameContext)
        {
            this.gameContext = gameContext;
            this.configContext = configContext;
        }

        protected override void Execute(List<Game.Entity> entities)
        {
            foreach (var entity in entities)
            {
                var transform = entity.GetTransform();
                entity.RemoveFree();

                var machineEntity = gameContext.CreateEntity()
                   .AddFree()
                   .AddMachine()
                   .AddMotor(int.MaxValue)//Will be replaced when linking
                   .AddTransform(transform.position, transform.rotation)
                   .AddId(entity.GetId().Value);

                var commodity = gameContext.GetCommodityTypeAndValueRelatedToEntity(entity);
                switch (commodity.type)
                {
                    case CommodityType.Taco:
                        machineEntity.AddTaco(commodity.value);
                        machineEntity.AddVisualRepresentation(VisualType.TacoMachine);
                        break;
                    case CommodityType.Soda:
                        machineEntity.AddSoda(commodity.value);//just makes 1 soda at a time
                        machineEntity.AddVisualRepresentation(VisualType.SodaMachine);
                        break;
                }

            }
        }

        protected override bool Filter(Game.Entity entity)
        {
            return entity.HasCreate() && entity.HasMachine() && entity.HasFree() && entity.HasSpot();
        }

        protected override ICollector<Game.Entity> GetTrigger(IContext<Game.Entity> context)
        {
            return context.CreateCollector(Matcher.AllOf(Free, Machine, Spot, Create));
        }
    }
}
