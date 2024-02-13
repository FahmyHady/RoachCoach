using Entitas;
using RoachCoach.Game;
using System.Collections.Generic;
using UnityEngine;
using static RoachCoach.RoachCoachGameDelayMatcher;
namespace RoachCoach
{
    //This  delay action part of the system is a last resort, because I spent way too much time on this already
    public class DelaySystem : IExecuteSystem
    {
        readonly GameContext gameContext;
        Dictionary<Game.Entity, float> delayedEntitiesWithElapsedTime = new Dictionary<Game.Entity, float>();
        public DelaySystem(GameContext gameContext) : base()
        {
            this.gameContext = gameContext;
        }

        public void Execute()
        {
            var entities = gameContext.GetEntities(Game.Matcher.AllOf(Delay));
            foreach (var entity in entities)
            {
                if(delayedEntitiesWithElapsedTime.ContainsKey(entity))
                    delayedEntitiesWithElapsedTime[entity] += Time.deltaTime;
                else
                    delayedEntitiesWithElapsedTime.Add(entity,Time.deltaTime);

                if (delayedEntitiesWithElapsedTime[entity] > entity.GetDelay().ValueInSeconds)
                {
                    delayedEntitiesWithElapsedTime.Remove(entity);
                    entity.RemoveDelay();
                    if (entity.HasDelayedDestroy())
                    {
                        entity.RemoveDelayedDestroy();
                        entity.AddDestroyed();
                    }
                }
            }
        }





    }
}
