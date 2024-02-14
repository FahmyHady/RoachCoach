using Entitas;
using RoachCoach.Game;
using System.Collections.Generic;
using UnityEngine;
using static RoachCoach.RoachCoachGameMachineMatcher;
using static RoachCoach.RoachCoachGameMachineStandMatcher;
using static RoachCoach.RoachCoachGameRelatedSpotMatcher;
using static RoachCoach.RoachCoachGameTacoMatcher;
using static RoachCoach.RoachCoachGameSodaMatcher;
using static RoachCoach.RoachCoachGameFreeMatcher;
using System.Runtime.Remoting.Contexts;
namespace RoachCoach
{
    public class CreateMachineStandSystem : ReactiveSystem<Game.Entity>
    {
        readonly GameContext gameContext;
        private readonly ConfigContext configContext;
        bool sodaStandCreated;
        bool tacoStandCreated;
        public CreateMachineStandSystem(GameContext gameContext, ConfigContext configContext) : base(gameContext)
        {
            this.gameContext = gameContext;
            this.configContext = configContext;
        }

        protected override void Execute(List<Game.Entity> entities)
        {
            foreach (var item in entities)
            {
                var machineType = gameContext.GetCommodityTypeAndValueRelatedToEntity(item);
                switch (machineType.type)
                {
                    case CommodityType.Taco:
                        if (TacoMachinesCount() == 1)
                            CreateTacoMachineStand();
                        break;
                    case CommodityType.Soda:
                        if (SodaMachinesCount() == 1)
                            CreateSodaMachineStand();
                        break;
                }
                if(tacoStandCreated && sodaStandCreated)
                {
                    this.Deactivate();
                    break;
                }
            }

        }

        protected override bool Filter(Game.Entity entity)
        {
            return entity.HasRelatedSpot() && entity.HasMachine();
        }

        protected override ICollector<Game.Entity> GetTrigger(IContext<Game.Entity> context)
        {
            return context.CreateCollector(Matcher.AllOf(Machine, RelatedSpot).AnyOf(Soda, Taco));
        }

        int TacoMachinesCount()
        {
            return gameContext.GetEntities((Matcher.AllOf(Machine, Taco, RelatedSpot))).Length;
        }
        int SodaMachinesCount()
        {
            return gameContext.GetEntities((Matcher.AllOf(Machine, Soda, RelatedSpot))).Length;
        }
        void CreateTacoMachineStand()
        {
            if (tacoStandCreated) return;
            tacoStandCreated = true;
            var tacoMachineStandSpot = gameContext.GetEntities((Matcher.AllOf(MachineStand, Taco, Free)))[0];
            tacoMachineStandSpot.RemoveFree();
            var transform = tacoMachineStandSpot.GetTransform();
            gameContext.CreateEntity().AddTransform(transform.position, transform.rotation).AddMachineStand().AddTaco(int.MaxValue).AddVisualRepresentation(VisualType.TacoMachineStand);
        }
        void CreateSodaMachineStand()
        {
            if (sodaStandCreated) return;
            sodaStandCreated = true;
            var sodaMachineStandSpot = gameContext.GetEntities((Matcher.AllOf(MachineStand, Soda, Free)))[0];
            sodaMachineStandSpot.RemoveFree();
            var transform = sodaMachineStandSpot.GetTransform();
            gameContext.CreateEntity().AddTransform(transform.position, transform.rotation).AddMachineStand().AddSoda(int.MaxValue).AddVisualRepresentation(VisualType.SodaMachineStand);
        }
    }
}
