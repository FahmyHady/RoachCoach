using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static RoachCoach.RoachCoachGameFreeMatcher;
using static RoachCoach.RoachCoachGameMachineMatcher;
using static RoachCoach.RoachCoachGameSpotMatcher;
using static RoachCoach.RoachCoachGameSodaMatcher;
using static RoachCoach.RoachCoachGameTacoMatcher;
using static RoachCoach.RoachCoachGameChefMatcher;
using static RoachCoach.RoachCoachGameOutletMatcher;
namespace RoachCoach
{
    public enum UpgradeType
    {
        IncreaseChefMoveSpeed, IncreaseChefTakeOrderSpeed,
        AddCustomer, AddChef, AddSodaMachine, AddTacoMachine, IncreaseSodaPrice, IncreaseTacoPrice
    }
    [System.Serializable]
    public class ShopUpgradeData
    {
        public int cost;
        [TextArea]
        public string description;
        public UpgradeType type;
    }
    public class ShopUpgradesMonobehaviour : MonoBehaviour
    {
        public static UnityEvent<ShopUpgradeData> OnUpgradeAdded = new UnityEvent<ShopUpgradeData>();
        public static UnityEvent<ShopUpgradeData> OnUpgradeRemoved = new UnityEvent<ShopUpgradeData>();
        public static UnityEvent<ShopUpgradeData> OnUpgradeChanged = new UnityEvent<ShopUpgradeData>();

        [SerializeField] ShopConfigMonobehaviour shopConfig;
        [SerializeField] List<ShopUpgradeData> upgrades = new List<ShopUpgradeData>();
        private void Start()
        {
            foreach (var item in upgrades)
            {
                OnUpgradeAdded?.Invoke(item);
            }
        }
        private void OnEnable()
        {
            UpgradesUIManager.OnUpgradeRequested.AddListener(ResolveUpgradeRequest);
        }
        private void OnDisable()
        {
            UpgradesUIManager.OnUpgradeRequested.RemoveListener(ResolveUpgradeRequest);
        }
        private void ResolveUpgradeRequest(ShopUpgradeData upgradeData)
        {
            int walletValue = GameContext.Instance.GetWallet().Value;
            GameContext.Instance.ReplaceWallet(walletValue - upgradeData.cost);
            bool maxed = false;
            switch (upgradeData.type)
            {
                case UpgradeType.IncreaseChefMoveSpeed:
                    maxed = UpgradeChefSpeed();
                    break;
                case UpgradeType.IncreaseChefTakeOrderSpeed:
                    maxed = UpgradeChefTakeOrderSpeed();
                    break;
                case UpgradeType.AddCustomer:
                    maxed = AddCustomer();
                    break;
                case UpgradeType.AddChef:
                    maxed = AddChef();
                    break;
                case UpgradeType.AddSodaMachine:
                    maxed = AddSodaMachine();
                    break;
                case UpgradeType.AddTacoMachine:
                    maxed = AddTacoMachine();
                    break;
                case UpgradeType.IncreaseSodaPrice:
                    maxed = IncreaseSodaPrice();
                    break;
                case UpgradeType.IncreaseTacoPrice:
                    maxed = IncreaseTacoPrice();
                    break;
            }
            if (maxed)
            {
                upgrades.Remove(upgradeData);
                OnUpgradeRemoved?.Invoke(upgradeData);
            }
            else
            {
                upgradeData.cost *= 2;
                OnUpgradeChanged?.Invoke(upgradeData);
            }
        }
        public bool UpgradeChefSpeed()
        {
            float newSpeed = shopConfig.ChefMovementSpeed + 0.5f;
            shopConfig.ChefMovementSpeed = newSpeed;
            if (newSpeed > 10)//we can setup the max in config or elsewhere
                return true;
            return false;
        }
        public bool UpgradeChefTakeOrderSpeed()
        {
            float newDuration = shopConfig.ChefOrderTakingDuration - 0.5f;
            shopConfig.ChefOrderTakingDuration = newDuration;
            if (newDuration <= 1)//we can setup the max in config or elsewhere
                return true;
            return false;
        }
        public bool AddCustomer()
        {
            int newCount = shopConfig.CurrentCustomerCount + 1;
            shopConfig.CurrentCustomerCount = newCount;
            if (newCount >= shopConfig.MaxCustomerCount)
                return true;
            return false;
        }
        public bool AddChef()
        {
            int newCount = shopConfig.CurrentChefNumber + 1;
            var chefCreationSpots = GameContext.Instance.GetEntities(Game.Matcher.AllOf(Free, Chef, Spot).NoneOf(Outlet, Machine));
            chefCreationSpots.RandomElement().AddCreate();
            shopConfig.CurrentChefNumber = newCount;
            if (newCount >= shopConfig.MaxChefNumber)
                return true;
            return false;
        }
        public bool AddSodaMachine()
        {
            var sodaMachineSpots = GameContext.Instance.GetEntities(Game.Matcher.AllOf(Free, Soda, Machine, Spot));
            sodaMachineSpots[0].AddCreate();
            if (sodaMachineSpots.Length == 1)//no more spots
                return true;
            return false;
        }
        public bool AddTacoMachine()
        {
            var tacoMachineSpots = GameContext.Instance.GetEntities(Game.Matcher.AllOf(Free, Taco, Machine, Spot));
            tacoMachineSpots[0].AddCreate();
            if (tacoMachineSpots.Length == 1)//no more spots
                return true;
            return false;
        }
        public bool IncreaseSodaPrice()
        {
            shopConfig.SodaPrice *= 2;
            if (shopConfig.SodaPrice > 100)//we can setup the max in config or elsewhere
                return true;
            return false;
        }
        public bool IncreaseTacoPrice()
        {
            shopConfig.TacoPrice *= 2;
            if (shopConfig.TacoPrice > 300)//we can setup the max in config or elsewhere
                return true;
            return false;
        }


    }
}
