using Entitas;
using Entitas.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoachCoach
{
    public class Visual : MonoBehaviour, IVisual, IRoachCoachGameTransformAddedListener, IRoachCoachGameDestroyedAddedListener
    {
        Game.Entity entity;
        public virtual void Link(Game.Entity entity)
        {
            this.entity = entity;
            gameObject.Link(entity);
            this.entity.AddTransformAddedListener(this);
            this.entity.AddDestroyedAddedListener(this);

            var transformComponent = entity.GetTransform();
            transform.localPosition = transformComponent.position;
            transform.localRotation = transformComponent.rotation;
        }


        public virtual void OnTransformAdded(Game.Entity entity, Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
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
    }
}
