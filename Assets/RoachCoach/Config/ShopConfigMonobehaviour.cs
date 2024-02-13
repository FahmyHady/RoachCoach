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


        [SerializeField] int maxChefNumber;
        [SerializeField] int currentChefNumber;
        [SerializeField] int maxCustomerNumber;
        [SerializeField] int currentCustomerNumber;
        [SerializeField] float chefOrderTakingDuration;
        [SerializeField] float chefMoveSpeed;
        [SerializeField] float customerSpeed;
        [SerializeField] int tacoPrice;
        [SerializeField] int sodaPrice;
        [SerializeField] Transform[] chefCreationSpots;
        [SerializeField] MachineCreationData[] machineStandsCreationSpots;
        [SerializeField] Transform outletSpot;

        Dictionary<CommodityType, Vector2Int> possibleCommodityTypesAndMinMaxValues = new Dictionary<CommodityType, Vector2Int>();//This should be adjusted when a new machine is added

        public int MaxChefNumber
        {
            get => maxChefNumber; set { maxChefNumber = value; }
        }
        public int CurrentChefNumber
        {
            get => currentChefNumber; set { currentChefNumber = value; }
        }
        public int MaxCustomerCount
        {
            get => maxCustomerNumber; set { maxCustomerNumber = value; }
        }
        public int CurrentCustomerCount { get => currentCustomerNumber; set { currentCustomerNumber = value; } }

        public float ChefMovementSpeed { get => chefMoveSpeed; set { chefMoveSpeed = value; } }
        public float ChefOrderTakingDuration { get => chefOrderTakingDuration; set { chefOrderTakingDuration = value; } }

        public float CustomerMovementSpeed { get => customerSpeed; set { customerSpeed = value; } }

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


        public MachineCreationEntityData[] GetMachineCreationData()
        {
            MachineCreationEntityData[] values = new MachineCreationEntityData[machineStandsCreationSpots.Sum(a => a.machinesCreationSpots.Length)];
            int index = 0;
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
                    values[index] = currentMachine;
                    index++;
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
            {
                var vector2Int = possibleCommodityTypesAndMinMaxValues[commodityType];
                vector2Int = new Vector2Int(vector2Int.x, vector2Int.y + 2);//increase order max count
                possibleCommodityTypesAndMinMaxValues[commodityType] = vector2Int;
            }
            else
                possibleCommodityTypesAndMinMaxValues.Add(commodityType, new Vector2Int(1, 2));

        }
        public int TacoPrice { get => tacoPrice; set => tacoPrice = value; }
        public int SodaPrice { get => sodaPrice; set => sodaPrice = value; }


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
