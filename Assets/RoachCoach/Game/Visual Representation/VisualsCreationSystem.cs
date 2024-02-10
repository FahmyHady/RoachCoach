using Entitas;
using System;
using System.Collections.Generic;
using UnityEngine;
using static RoachCoach.RoachCoachGameVisualRepresentationMatcher;
using RoachCoach.Game;
namespace RoachCoach
{
    public class VisualsCreationSystem : ReactiveSystem<Game.Entity>
    {
        Dictionary<VisualType, Transform> visualParents = new Dictionary<VisualType, Transform>();
        public VisualsCreationSystem(IContext<Game.Entity> context) : base(context)
        {
            foreach (var item in Enum.GetNames(typeof(VisualType)))
            {
                visualParents.Add((VisualType)Enum.Parse(typeof(VisualType), item), new GameObject(item + 's').transform);
            }
        }

        protected override void Execute(List<Game.Entity> entities)
        {
            foreach (var entity in entities)
                entity.AddVisualReference(InstantiateVisual(entity));
        }
        IVisual InstantiateVisual(Game.Entity entity)
        {
            var visualType = entity.GetVisualRepresentation().Type;
            //Could've used Resources.Load but don't want to rely on magic strings
            GameObject prefab = ConfigContext.Instance.GetPrefabConfig().Value.GetPrefab(visualType);
            var visual = GameObject.Instantiate(prefab, visualParents[visualType]).GetComponent<IVisual>();
            visual.Link(entity);
            return visual;
        }
        protected override bool Filter(Game.Entity entity)
        {
            return entity.HasVisualRepresentation();
        }

        protected override ICollector<Game.Entity> GetTrigger(IContext<Game.Entity> context)
        {
            return context.CreateCollector(Matcher.AnyOf(VisualRepresentation));
        }
    }
}
