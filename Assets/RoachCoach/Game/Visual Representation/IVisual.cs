using Entitas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoachCoach
{
    public interface IVisual 
    {
        public GameObject GetConnectedObject();
        public void Link(Game.Entity entity);
    }
}
