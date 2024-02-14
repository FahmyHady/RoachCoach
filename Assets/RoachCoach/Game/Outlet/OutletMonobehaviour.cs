using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoachCoach
{
    public class OutletMonobehaviour : Visual, IOutletVisual
    {
        [System.Serializable]
        public class RelatedTransforms
        {
            public Transform one;
            public Transform two;
        }
        [SerializeField] RelatedTransforms[] customerSpots;

        public (Vector3 customerSpotPos, Quaternion customerSpotRot, Vector3 chefSpotPos, Quaternion chefSpotRot)[] GetSpotLocations()
        {
            (Vector3 customerSpotPos, Quaternion customerSpotRot, Vector3 chefSpotPos, Quaternion chefSpotRot)[] spots = new (Vector3 customerSpotPos, Quaternion customerSpotRot, Vector3 chefSpotPos, Quaternion chefSpotRot)[customerSpots.Length];
            for (int i = 0; i < customerSpots.Length; i++)
            {
                spots[i].customerSpotPos = customerSpots[i].one.position;
                spots[i].customerSpotRot = customerSpots[i].one.rotation;
                spots[i].chefSpotPos = customerSpots[i].two.position;
                spots[i].chefSpotRot = customerSpots[i].two.rotation;
            }
            return spots;
        }
    }
}
