
using RoachCoach.Game;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace RoachCoach
{
    public class CharacterVisualRepresentation : Visual, IRoachCoachGameDelayAddedListener, IRoachCoachGameDelayRemovedListener
    {
        [SerializeField] GameObject canvas;
        [SerializeField] Image circleFill;
        float speed;
        Vector3 lastPos;
        Animator animator;
        bool init;
        private async void Awake()
        {
            animator = GetComponent<Animator>();
            animator.speed = 0;
            await Task.Delay(Random.Range(500, 3000));
            animator.speed = 1;
            init= true;
        }
        public override void Link(Entitas.Entity entity)
        {
            base.Link(entity);
            this.linkedEntity.AddDelayAddedListener(this);
            this.linkedEntity.AddDelayRemovedListener(this);

        }
        public void OnDelayAdded(Entity entity, float valueInSeconds)
        {
            if (canvas == null) return;
            StopAllCoroutines();
            StartCoroutine(DoOverTime(entity.GetDelay().ValueInSeconds));
        }

        public void OnDelayRemoved(Entity entity)
        {
            if (canvas == null) return;

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
                circleFill.fillAmount = 1 - (elapsed / duration);
                yield return null;
            }
        }
        private void Update()
        {
            if (!init) return;
            speed = (transform.position - lastPos).magnitude;
            lastPos = transform.position;
            if (speed < 0.01f)
                animator.speed = 0.5f;
            else
                animator.speed = 2.5f;
        }
    }

}
