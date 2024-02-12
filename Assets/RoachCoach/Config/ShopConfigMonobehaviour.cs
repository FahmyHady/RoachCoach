using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Graphs;
using UnityEngine;

namespace RoachCoach
{
    public class ShopConfigMonobehaviour : MonoBehaviour, IShopConfig
    {
        [System.Serializable]
        public class MachineCreationData
        {
            public CommodityType commodityType;
            public Transform machineStandSpot;
            public Transform[] machinesCreationSpots;
        }
        [SerializeField, Range(0, 3)] int startingChefsNumber = 1;
        [SerializeField, Range(0, 5)] float chefOrderTakingDuration = 1;
        [SerializeField] float chefMoveSpeed = 3;
        [SerializeField] float customerSpeed = 4;
        [SerializeField, Range(0, 3)] int maxCustomerNumber = 1;
        [SerializeField, Range(0, 3)] int startingMachineStandsNumber = 1;

        [SerializeField] Transform[] chefCreationSpots;
        [SerializeField] MachineCreationData[] machineStandsCreationSpots;
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

        public (Vector3, Quaternion)[] GetChefCreationSpots()
        {
            (Vector3, Quaternion)[] values = new (Vector3, Quaternion)[chefCreationSpots.Length];
            for (int i = 0; i < chefCreationSpots.Length; i++)
            {
                values[i].Item1 = chefCreationSpots[i].position;
                values[i].Item2 = chefCreationSpots[i].rotation;
            }
            return values;
        }

        public (Vector3, Quaternion)[] GetCustomerCreationSpots()
        {
            (Vector3, Quaternion)[] values = new (Vector3, Quaternion)[customerCreationSpots.Length];
            for (int i = 0; i < customerCreationSpots.Length; i++)
            {
                values[i].Item1 = customerCreationSpots[i].position;
                values[i].Item2 = customerCreationSpots[i].rotation;
            }
            return values;
        }
        public (CommodityType type, Vector3 posOfMachine, Quaternion rotationOfMachine, Vector3 posOfMachineStand, Quaternion rotationOfMachineStand)[] GetMachineCreationData()
        {
            (CommodityType type, Vector3 posOfMachine, Quaternion rotationOfMachine, Vector3 posOfMachineStand, Quaternion rotationOfMachineStand)[] values = new (CommodityType type, Vector3 posOfMachine, Quaternion rotationOfMachine, Vector3 posOfMachineStand, Quaternion rotationOfMachineStand)[machineStandsCreationSpots.Sum(a => a.machinesCreationSpots.Length)];

            for (int i = 0; i < machineStandsCreationSpots.Length; i++)
            {
                var relatedData = machineStandsCreationSpots[i];

                for (int j = 0; j < relatedData.machinesCreationSpots.Length; j++)
                {
                    var currentMachine = values[j];
                    currentMachine.type = relatedData.commodityType;
                    currentMachine.posOfMachineStand = relatedData.machineStandSpot.position;
                    currentMachine.rotationOfMachineStand = relatedData.machinesCreationSpots[j].rotation;
                    currentMachine.posOfMachine = relatedData.machinesCreationSpots[j].position;
                    currentMachine.rotationOfMachine = relatedData.machinesCreationSpots[j].rotation;
                }
            }

            return values;
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
