using Entitas;
using Entitas.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoachCoach
{
    public class Visual : MonoBehaviour, IVisual, IRoachCoachGameTransformAddedListener, IRoachCoachGameDestroyedAddedListener
    {
        protected Game.Entity linkedEntity;
        public virtual void Link(Entity entity)
        {
            gameObject.Link(entity);
            this.linkedEntity = (Game.Entity)entity;
            this.linkedEntity.AddTransformAddedListener(this);
            this.linkedEntity.AddDestroyedAddedListener(this);

            var transformComponent = linkedEntity.GetTransform();
            transform.localPosition = transformComponent.position;
            transform.localRotation = transformComponent.rotation;
        }

        public void OnDestroyedAdded(Game.Entity entity)
        {
            Destroy(gameObject); //This will trigger the Monobehaviour OnDestroy
        }

        //GameObject normally destroyed from Unity
        protected virtual void OnDestroy()
        {
            gameObject.Unlink();
        }

        public GameObject GetConnectedObject()
        {
            return gameObject;
        }

        public virtual void OnTransformAdded(Game.Entity entity, Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
        }
    }
}
