using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoachCoach
{
    public class MachineStandQueue : IQueue
    {
        Queue<Entity> entitiesInQueue = new Queue<Entity>();
        public void AddToQueue(Entity entity)
        {
        }

        public Entity GetNextInQueue()
        {
            return null;
        }

        public bool HasFreeSpot(out SpotComponent spot)
        {
            spot = null;
            return false;
        }

        public void IncrementSpots(SpotComponent spotToAdd)
        {
        }
    }
}
