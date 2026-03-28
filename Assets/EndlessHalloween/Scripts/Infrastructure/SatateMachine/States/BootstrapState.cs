using Assets.SpaceForge.Scripts;
using DIContainer;
using Infrastructure.PageProvider;
using Logic.Localization;
using UnityEngine;

namespace Infrastructure.SatateMachine.States
{
    public class BootstrapState : IState
    {
        private GameStateMachine _stateMachine;
        private SceneLoader _sceneLoader;
        private DI _container;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, DI container)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _container = container;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Const.InitialScene, EnterLoadLevel);
        }

        public void Exit()
        {
            
        }

        private void EnterLoadLevel()
        {
            
            _stateMachine.Enter<LoadLevelState, string>(Const.Scene.Welcome);
        }

        private void RegisterServices()
        {
            DI.Container.RegisterSingle<ILocalizationService>(new RuLocalizationService());
            
        }
    }
}
