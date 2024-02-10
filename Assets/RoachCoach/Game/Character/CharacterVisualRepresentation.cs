

using RoachCoach.Game;
using UnityEngine;

namespace RoachCoach
{
    public class CharacterVisualRepresentation : Visual
    {
        public override void OnTransformAdded(Entity entity, Vector3 position, Quaternion rotation)
        {
            Debug.Log("Come on man");
            base.OnTransformAdded(entity, position, rotation);
        }
    }
}
