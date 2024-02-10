using Entitas;
using System;
using System.Collections.Generic;
using UnityEngine;
using static RoachCoach.RoachCoachGameVisualRepresentationMatcher;

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
                CreateAndLink(entity);
        }
        void CreateAndLink(Game.Entity entity)
        {
            //Could've used Resources.Load but don't want to rely on magic strings
            var visualType = entity.GetVisualRepresentation().Type;
            GameObject prefab = ConfigContext.Instance.GetPrefabConfig().Value.GetPrefab(visualType);
            var gameObject = GameObject.Instantiate(prefab, visualParents[visualType]);
            gameObject.GetComponent<IVisual>().Link(entity);

        }
        protected override bool Filter(Game.Entity entity)
        {
            return entity.HasVisualRepresentation();
        }

        protected override ICollector<Game.Entity> GetTrigger(IContext<Game.Entity> context)
        {
            return context.CreateCollector(VisualRepresentation);
        }
    }
}
