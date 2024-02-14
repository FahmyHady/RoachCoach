using Entitas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoachCoach
{
    public interface IVisual
    {
        public void Link(Entity entity);
    }
    public interface IOutletVisual : IVisual
    {
        public (Vector3 customerSpotPos, Quaternion customerSpotRot, Vector3 chefSpotPos, Quaternion chefSpotRot)[] GetSpotLocations();
    }
    public interface IMachineVisual : IVisual
    {
        public MachineMonobehaviour.MachineData GetMachineData();
        public (Vector3 pos,Quaternion rot) GetPreparationSpotLocation();
    }

}
