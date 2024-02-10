

using Entitas;
using static RoachCoach.RoachCoachGameOutletMatcher;
using static RoachCoach.RoachCoachGameFreeMatcher;
using static RoachCoach.RoachCoachGameChefMatcher;
using RoachCoach.Game;
using System.Diagnostics;
namespace RoachCoach
{
    public class ChefBrainSystem : IExecuteSystem
    {
        private readonly GameContext gameContext;

        public ChefBrainSystem(GameContext gameContext) : base()
        {
            this.gameContext = gameContext;
        }
        public void Execute()
        {
            if (!UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Space)) return;
            var emptyCashierSpots = gameContext.GetEntities(Matcher.AllOf(RoachCoach.RoachCoachGameSpotMatcher.Spot, Free));

            GoToSpot(emptyCashierSpots.RandomElement());
            //item.AddFree();
        }
        void GoToSpot(Game.Entity spot)
        {
            var transform = spot.GetTransform();
            spot.RemoveFree();
            var chef = gameContext.GetEntities(Matcher.AllOf(Chef).NoneOf(RoachCoach.RoachCoachGameSpotMatcher.Spot)).RandomElement();
           UnityEngine.Debug.Log("Should be called here the event");
            chef.ReplaceTransform(transform.position, transform.rotation);
        }
    }
}
