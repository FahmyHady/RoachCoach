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

            gameContext.CreateContextObserver();
            configContext.CreateContextObserver();
            configContext.SetGameConfig(gameConfig);
            configContext.SetPrefabConfig(prefabConfig);
            systems = new GameSystems(gameContext, gameConfig);
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
