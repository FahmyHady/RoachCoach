using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoachCoach
{
    public class OutletMonobehaviour : Visual
    {
        [System.Serializable]
        public class RelatedTransforms
        {
            public Transform one;
            public Transform two;
        }
        public RelatedTransforms[] customerSpots;
    }
}
