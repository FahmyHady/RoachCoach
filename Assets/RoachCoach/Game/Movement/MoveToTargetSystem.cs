using Entitas;
using RoachCoach.Game;
using System.Collections.Generic;
using UnityEngine;
using static RoachCoach.RoachCoachGameMotorMatcher;
using static RoachCoach.RoachCoachGameTargetLocationMatcher;
namespace RoachCoach
{
    public class MoveToTargetSystem : IExecuteSystem
    {
        readonly GameContext gameContext;

        public MoveToTargetSystem(GameContext gameContext) : base()
        {
            this.gameContext = gameContext;
        }

        public void Execute()
        {
            var entities = gameContext.GetEntities(Game.Matcher.AllOf(Motor, TargetLocation));
            Vector3 pos = default;
            Quaternion rot = default;
            TargetLocationComponent targetLocation = null;
            TransformComponent currentTransform = null;
            foreach (var entity in entities)
            {
                currentTransform = entity.GetTransform();
                targetLocation = entity.GetTargetLocation();
                if (Vector3.Distance(currentTransform.position, targetLocation.targetPos) > Vector3.kEpsilon)
                {
                    pos = Vector3.MoveTowards(currentTransform.position, targetLocation.targetPos, entity.GetMotor().Value * Time.deltaTime);
                    rot = Quaternion.LookRotation((targetLocation.targetPos - currentTransform.position).normalized, Vector3.up);
                    entity.ReplaceTransform(pos, rot);
                }
                else
                {
                    entity.RemoveTargetLocation();
                }
            }
        }





    }
}
