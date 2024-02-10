using UnityEngine;

namespace RoachCoach
{
    [CreateAssetMenu(menuName = "Roach Coach/Prefabs Config")]
    public class ScriptablePrefabConfig : ScriptableObject, IPrefabConfig
    {
        public GameObject[] chefPrefabs;
        public GameObject[] customerPrefabs;
        public GameObject itemBox;
        public GameObject machine;
        public GameObject machineStand;
        public GameObject GetPrefab(VisualType type)
        {
            switch (type)
            {
                case VisualType.Chef:
                    return chefPrefabs.RandomElement();
                case VisualType.Customer:
                    return customerPrefabs.RandomElement();
                case VisualType.ItemBox:
                    return itemBox;
                case VisualType.Machine:
                    return machine;
                case VisualType.MachineStand:
                    return machineStand;
                default:
                    return null;
            }
        }
    }
}
