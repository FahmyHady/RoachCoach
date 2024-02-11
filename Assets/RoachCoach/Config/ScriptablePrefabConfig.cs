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
        public GameObject outlet;
        public GameObject sodaOrder;
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
                case VisualType.TacoMachine:
                    return machine;
                case VisualType.TacoMachineStand:
                    return machineStand;
                case VisualType.Outlet:
                    return outlet;
                case VisualType.Soda:
                    return sodaOrder;
                case VisualType.Taco:
                case VisualType.SodaMachine:
                case VisualType.SodaMachineStand:
                default:
                    return null;
            }
        }
    }
}
