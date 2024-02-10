using Entitas;
using Entitas.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoachCoach
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] MonoBehaviourGameConfig gameConfig;
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

            configContext.SetGameConfig(gameConfig);
            configContext.SetPrefabConfig(prefabConfig);
            systems = new GameSystems(gameContext, configContext,inputContext);
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
    }
}
