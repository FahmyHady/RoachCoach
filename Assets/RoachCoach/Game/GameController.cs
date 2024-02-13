using Entitas;
using Entitas.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;


namespace RoachCoach
{
    public class GameController : MonoBehaviour, IRoachCoachGameAnyMachineAddedListener
    {
        [SerializeField] ShopConfigMonobehaviour shopConfig;
        [SerializeField] ScriptablePrefabConfig prefabConfig;
        Systems systems;


        private void Awake()
        {
            ContextInitialization.InitializeContexts();

            var gameContext = GameContext.Instance;
            var configContext = ConfigContext.Instance;
            var inputContext = InputContext.Instance;

            gameContext.CreateContextObserver();
            configContext.CreateContextObserver();
            inputContext.CreateContextObserver();

            configContext.SetShopConfig(shopConfig);
            configContext.SetPrefabConfig(prefabConfig);
            systems = new GameSystems(gameContext, configContext, inputContext);
            gameContext.SetWallet(0);
            gameContext.CreateEntity().AddAnyMachineAddedListener(this);
        }
        private void Start()
        {

            systems.Initialize();
        }
        private void Update()
        {
            systems.Execute();
            systems.Cleanup();
        }
        public void OnAnyMachineAdded(Game.Entity entity)
        {
            //This is a machine spot and not a machine, probably should Identify machines with a mechanical flag or something
            if (entity.HasSpot()) return;
            if (entity.HasSoda())
                shopConfig.AddToPossibleOrders(CommodityType.Soda);
            else if (entity.HasTaco())
                shopConfig.AddToPossibleOrders(CommodityType.Taco);
        }
    }
}
