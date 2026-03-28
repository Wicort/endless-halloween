using DIContainer;
using Infrastructure;
using Infrastructure.AssetProvider;
using Infrastructure.SatateMachine;
using Infrastructure.SatateMachine.States;
using Logic;
using UnityEngine;

namespace Assets.SpaceForge.Scripts
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public static GameBootstrapper Instance;

        private GameStateMachine _gameStateMashine;

        private LoadingCurtain _curtain;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Initialize();
                DontDestroyOnLoad(this);
            } else
            {
                Destroy(this);
            }
        }

        private void Initialize()
        {
            LoadingCurtain curtainPrefab = Resources.Load<LoadingCurtain>(AssetPath.LoadingCurtainPath);
            _curtain = Instantiate(curtainPrefab);
            _gameStateMashine = new GameStateMachine(new SceneLoader(this), _curtain, DI.Container);
            _gameStateMashine.Enter<BootstrapState>();
        }
    }
}
