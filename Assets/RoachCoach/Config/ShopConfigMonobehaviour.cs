using System;
using System.Collections.Generic;
using System.Linq;
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

        Dictionary<CommodityType, Vector2Int> possibleCommodityTypesAndMinMaxValues;//This should be adjusted when a new machine is added

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
        public MachineCreationEntityData[] GetMachineCreationData()
        {
            MachineCreationEntityData[] values = new MachineCreationEntityData[machineStandsCreationSpots.Sum(a => a.machinesCreationSpots.Length)];

            for (int i = 0; i < machineStandsCreationSpots.Length; i++)
            {
                var relatedData = machineStandsCreationSpots[i];

                for (int j = 0; j < relatedData.machinesCreationSpots.Length; j++)
                {
                    MachineCreationEntityData currentMachine = default;
                    currentMachine.type = relatedData.commodityType;
                    currentMachine.posOfMachineStand = relatedData.machineStandSpot.position;
                    currentMachine.rotationOfMachineStand = relatedData.machinesCreationSpots[j].rotation;
                    currentMachine.posOfMachine = relatedData.machinesCreationSpots[j].position;
                    currentMachine.rotationOfMachine = relatedData.machinesCreationSpots[j].rotation;
                    values[j] = currentMachine;
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
            var order = possibleCommodityTypesAndMinMaxValues.RandomElement();
            return (order.Key, UnityEngine.Random.Range(order.Value.x, order.Value.y + 1));
        }

        public void AddToPossibleOrders(CommodityType commodityType)
        {
            //Should be a dictionary but I don't have a seriliazable one handy
            if (possibleCommodityTypesAndMinMaxValues.ContainsKey(commodityType))
                possibleCommodityTypesAndMinMaxValues[commodityType] += Vector2Int.one;
            else
                possibleCommodityTypesAndMinMaxValues.Add(commodityType, new Vector2Int(1, 2));
            Debug.Log("Order Added");

        }
    }

    public struct MachineCreationEntityData
    {
        public CommodityType type;
        public Vector3 posOfMachine;
        public Quaternion rotationOfMachine;
        public Vector3 posOfMachineStand;
        public Quaternion rotationOfMachineStand;

        public MachineCreationEntityData(CommodityType type, Vector3 posOfMachine, Quaternion rotationOfMachine, Vector3 posOfMachineStand, Quaternion rotationOfMachineStand)
        {
            this.type = type;
            this.posOfMachine = posOfMachine;
            this.rotationOfMachine = rotationOfMachine;
            this.posOfMachineStand = posOfMachineStand;
            this.rotationOfMachineStand = rotationOfMachineStand;
        }

        public override bool Equals(object obj)
        {
            return obj is MachineCreationEntityData other &&
                   type == other.type &&
                   posOfMachine.Equals(other.posOfMachine) &&
                   rotationOfMachine.Equals(other.rotationOfMachine) &&
                   posOfMachineStand.Equals(other.posOfMachineStand) &&
                   rotationOfMachineStand.Equals(other.rotationOfMachineStand);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(type, posOfMachine, rotationOfMachine, posOfMachineStand, rotationOfMachineStand);
        }

        public void Deconstruct(out CommodityType type, out Vector3 posOfMachine, out Quaternion rotationOfMachine, out Vector3 posOfMachineStand, out Quaternion rotationOfMachineStand)
        {
            type = this.type;
            posOfMachine = this.posOfMachine;
            rotationOfMachine = this.rotationOfMachine;
            posOfMachineStand = this.posOfMachineStand;
            rotationOfMachineStand = this.rotationOfMachineStand;
        }

        public static implicit operator (CommodityType type, Vector3 posOfMachine, Quaternion rotationOfMachine, Vector3 posOfMachineStand, Quaternion rotationOfMachineStand)(MachineCreationEntityData value)
        {
            return (value.type, value.posOfMachine, value.rotationOfMachine, value.posOfMachineStand, value.rotationOfMachineStand);
        }

        public static implicit operator MachineCreationEntityData((CommodityType type, Vector3 posOfMachine, Quaternion rotationOfMachine, Vector3 posOfMachineStand, Quaternion rotationOfMachineStand) value)
        {
            return new MachineCreationEntityData(value.type, value.posOfMachine, value.rotationOfMachine, value.posOfMachineStand, value.rotationOfMachineStand);
        }
    }
}
