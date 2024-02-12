using UnityEngine;

namespace RoachCoach
{
    [CreateAssetMenu(menuName = "Roach Coach/Prefabs Config")]
    public class ScriptablePrefabConfig : ScriptableObject, IPrefabConfig
    {
        [Header("Misc")]

        public GameObject[] chefPrefabs;
        public GameObject[] customerPrefabs;
        public GameObject outlet;
        public GameObject itemBox;
        [Space,Header("Machines")]
        public GameObject sodaMachine;
        public GameObject sodaMachineStand;
        public GameObject tacoMachine;
        public GameObject tacoMachineStand;
        [Space, Header("Orders")]
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
                    return tacoMachine;
                case VisualType.TacoMachineStand:
                    return tacoMachineStand;
                case VisualType.Outlet:
                    return outlet;
                case VisualType.Soda:
                    return sodaOrder;
                case VisualType.Taco:
                case VisualType.SodaMachine:
                    return sodaMachine;
                case VisualType.SodaMachineStand:
                    return sodaMachineStand;
                default:
                    return null;
            }
        }
    }
}
