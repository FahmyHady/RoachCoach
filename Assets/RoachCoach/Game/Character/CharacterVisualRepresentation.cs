
using RoachCoach.Game;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RoachCoach
{
    public class CharacterVisualRepresentation : Visual, IRoachCoachGameDelayAddedListener, IRoachCoachGameDelayRemovedListener
    {
        [SerializeField] GameObject canvas;
        [SerializeField] Image circleFill;
        public override void Link(Entitas.Entity entity)
        {
            base.Link(entity);
            this.linkedEntity.AddDelayAddedListener(this);
            this.linkedEntity.AddDelayRemovedListener(this);

        }
        public void OnDelayAdded(Entity entity, float valueInSeconds)
        {
            StopAllCoroutines();
            StartCoroutine(DoOverTime(entity.GetDelay().ValueInSeconds));
        }

        public void OnDelayRemoved(Entity entity)
        {
            canvas.SetActive(false);
            StopAllCoroutines();
        }
        IEnumerator DoOverTime(float duration)
        {
            canvas.SetActive(true);
            float elapsed = 0;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                circleFill.fillAmount = (1 - elapsed) / duration;
                yield return null;
            }
        }
    }

}
