using Assets.SpaceForge.Scripts;
using Logic;
using System.Collections;
using UnityEngine;

namespace Infrastructure.SatateMachine.States
{
    public class WelcomeState : IState
    {
        private GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;

        public WelcomeState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        public void Enter()
        {
            _sceneLoader.StartCoroutine(StartGame());
        }

        public void Exit()
        {
            
        }

        private IEnumerator StartGame()
        {
            yield return new WaitForSeconds(3f);
            _stateMachine.Enter<LoadLevelState, string>(Const.Scene.Game);
        }
    }
}
