using Entitas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoachCoach
{
    public class MachineMonobehaviour : Visual, IMachineVisual
    {
        [System.Serializable]
        public struct MachineData
        {
            public CommodityType type;
            public float makingStuffSpeed;
        }
        [SerializeField] Transform cookSpot;
        [SerializeField] MachineData machineData;
        public override void Link(Entity entity)
        {
            base.Link(entity);
            linkedEntity.ReplaceMotor(machineData.makingStuffSpeed);
        }
        public Vector3 GetPreparationSpotLocation()
        {
            return cookSpot.position;
        }

        public MachineData GetMachineData()
        {
            return machineData;
        }
    }
}
