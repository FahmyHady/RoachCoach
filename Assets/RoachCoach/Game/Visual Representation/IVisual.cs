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
    public interface IOutletVisual:IVisual
    {
        public (Vector3 customerSpot,Vector3 chefSpot)[] GetSpotLocations();
    }
    public interface IMachineVisual : IVisual
    {
        public MachineMonobehaviour.MachineData GetMachineData();
        public Vector3 GetPreparationSpotLocation();
    }

}
