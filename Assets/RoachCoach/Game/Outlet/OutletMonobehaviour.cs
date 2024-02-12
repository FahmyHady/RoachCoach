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

        public (Vector3 customerSpot, Vector3 chefSpot)[] GetSpotLocations()
        {
            (Vector3 customerSpot, Vector3 chefSpot)[] spots = new (Vector3 customerSpot, Vector3 chefSpot)[customerSpots.Length];
            for (int i = 0; i < customerSpots.Length; i++)
            {
                spots[i].customerSpot = customerSpots[i].one.position;
                spots[i].chefSpot = customerSpots[i].two.position;
            }
            return spots;
        }
    }
}
