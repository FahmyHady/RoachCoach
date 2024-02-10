using Entitas;
using RoachCoach;

public interface IQueue
{
    void AddToQueue(Entity entity);
    Entity GetNextInQueue();
    bool HasFreeSpot(out SpotComponent spot);
    void IncrementSpots(SpotComponent spotToAdd);
}
