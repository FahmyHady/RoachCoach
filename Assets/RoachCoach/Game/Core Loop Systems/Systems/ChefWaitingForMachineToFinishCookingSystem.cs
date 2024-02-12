using Entitas;
using System.Collections.Generic;
using static RoachCoach.RoachCoachGameChefMatcher;
using static RoachCoach.RoachCoachGameTargetLocationMatcher;
using static RoachCoach.RoachCoachGameMovingToMakeAnOrderMatcher;
using static RoachCoach.RoachCoachGameCharacterMatcher;
namespace RoachCoach
{
    public class ChefWaitingForMachineToFinishCookingSystem : ReactiveSystem<Game.Entity>
    {
        private readonly GameContext gameContext;
        private readonly ConfigContext configContext;

        public ChefWaitingForMachineToFinishCookingSystem(GameContext gameContext, ConfigContext configContext) : base(gameContext)
        {
            this.gameContext = gameContext;
            this.configContext = configContext;
        }
        protected override void Execute(List<Game.Entity> entities)
        {
            foreach (var entity in entities)
            {
                entity.RemoveMovingToTakeAnOrder();
                var relatedMachine = entity.GetRelatedMachine().RelatedMachine;
                var machinePreparationTime = relatedMachine.GetMotor().Value;
                entity.AddDelay(machinePreparationTime);
                entity.ad();//chef is taking order
            }
        }

        protected override bool Filter(Game.Entity entity)
        {
            return !entity.HasTargetLocation() && entity.HasMovingToTakeAnOrder();
        }

        protected override ICollector<Game.Entity> GetTrigger(IContext<Game.Entity> context)
        {
            return context.CreateCollector(Game.Matcher.AllOf(Chef, Character, MovingToMakeAnOrder).NoneOf(TargetLocation));
        }


    }
}
