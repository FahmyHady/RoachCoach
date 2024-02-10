using System.Collections.Generic;
using System.Linq;
using UnityEditor.Graphs;
using UnityEngine;

namespace RoachCoach
{
    public class MonoBehaviourGameConfig : MonoBehaviour, IGameConfig
    {
        [SerializeField, Range(0, 3)] int startingChefsNumber = 1;
        [SerializeField, Range(0, 5)] float defaultChefSpeed = 2;
        [SerializeField, Range(0, 5)] float defaultCustomerSpeed = 2;
        [SerializeField, Range(0, 3)] int startingCustomerNumber = 1;
        [SerializeField, Range(0, 3)] int startingMachineStandsNumber = 1;
        [SerializeField] Spot[] chefCreationSpots;
        [SerializeField] Spot[] machineStandsCreationSpots;
        [SerializeField] Spot[] customerSpots;
        [SerializeField] Spot outletSpot;
        public int StartingChefNumber => startingChefsNumber;
        public int StartingCustomerCount => startingCustomerNumber;
        public int StartingMachineStands => startingMachineStandsNumber;

        public float DefaultChefSpeed => defaultChefSpeed;

        public float DefaultCustomerSpeed => defaultCustomerSpeed;

        public (Vector3, Quaternion) GetNextChefTransform()
        {
            var spot = chefCreationSpots.RandomElement();
            return (spot.spotTransform.position, spot.spotTransform.rotation);
        }

        public (Vector3, Quaternion) GetNextCustomerTransform()
        {
            var spot = customerSpots.RandomElement();
            return (spot.spotTransform.position, spot.spotTransform.rotation);
        }

        public (Vector3, Quaternion) GetNextMachineStandTransform()
        {
            var spot = machineStandsCreationSpots.First(a => !a.full);
            spot.full = true;
            return (spot.spotTransform.position, spot.spotTransform.rotation);
        }

        public (Vector3, Quaternion) GetNextMachineTransform()
        {
            throw new System.NotImplementedException();
        }

        public (Vector3, Quaternion) GetOutletTransform()
        {
            return (outletSpot.spotTransform.position, outletSpot.spotTransform.rotation);
        }
    }
    [System.Serializable]
    public struct Spot
    {
        public bool full;
        public Transform spotTransform;
    }
}
