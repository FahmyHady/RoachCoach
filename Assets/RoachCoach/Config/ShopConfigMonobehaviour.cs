using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Graphs;
using UnityEngine;

namespace RoachCoach
{
    public class ShopConfigMonobehaviour : MonoBehaviour, IShopConfig
    {
        [SerializeField, Range(0, 3)] int startingChefsNumber = 1;
        [SerializeField, Range(0, 5)] float chefOrderTakingDuration = 1;
        [SerializeField] float chefMoveSpeed = 3;
        [SerializeField] float customerSpeed = 4;
        [SerializeField, Range(0, 3)] int maxCustomerNumber = 1;
        [SerializeField, Range(0, 3)] int startingMachineStandsNumber = 1;

        [SerializeField] Transform[] chefCreationSpots;
        [SerializeField] Transform[] machineStandsCreationSpots;
        [SerializeField] Transform[] customerCreationSpots;
        [SerializeField] Transform outletSpot;

        [SerializeField] CommodityType[] possibleCommodityTypes;//This should be adjusted when a new machine is added
        [SerializeField] Vector2Int minMaxCommodityAmount; //This can be based of a curve for example

        public int StartingChefNumber => startingChefsNumber;
        public int MaxCustomerCount => maxCustomerNumber;
        public int StartingMachineStands => startingMachineStandsNumber;

        public float ChefMovementSpeed => chefMoveSpeed;
        public float ChefOrderTakingDuration => chefOrderTakingDuration;

        public float CustomerMovementSpeed => customerSpeed;

        public (Vector3, Quaternion) GetNextChefTransform()
        {
            var spot = chefCreationSpots.RandomElement();
            return (spot.position, spot.rotation);
        }

        public (Vector3, Quaternion) GetNextCustomerTransform()
        {
            var spot = customerCreationSpots.RandomElement();
            return (spot.position, spot.rotation);
        }

        public Tuple<Vector3, Quaternion>[] GetMachineStandsTransform()
        {
            Tuple<Vector3, Quaternion>[] transformComponent = new Tuple<Vector3, Quaternion>[machineStandsCreationSpots.Length];
            for (int i = 0; i < machineStandsCreationSpots.Length; i++)
            {
                transformComponent[i] = new Tuple<Vector3, Quaternion>(machineStandsCreationSpots[i].position, machineStandsCreationSpots[i].rotation);
            }

            return transformComponent;
        }

        public (Vector3, Quaternion) GetNextMachineTransform()
        {
            throw new System.NotImplementedException();
        }

        public (Vector3, Quaternion) GetOutletTransform()
        {
            return (outletSpot.position, outletSpot.rotation);
        }

        public (CommodityType, int) GetOrderData()
        {
            return (possibleCommodityTypes.RandomElement(), UnityEngine.Random.Range(minMaxCommodityAmount.x, minMaxCommodityAmount.y + 1));
        }
    }

}
