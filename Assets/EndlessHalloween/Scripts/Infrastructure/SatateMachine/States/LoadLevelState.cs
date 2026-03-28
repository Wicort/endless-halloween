using Assets.SpaceForge.Scripts;
using Logic;
using System.Collections;
using UnityEngine;

namespace Infrastructure.SatateMachine.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private string _sceneName;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        public void Enter(string sceneName)
        {
            //_curtain.Show();
            _sceneName = sceneName;
            _sceneLoader.StartCoroutine(LoadScene());
            //_sceneLoader.Load(_sceneName, OnLoaded);
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            switch (_sceneName)
            {
                case Const.Scene.Game:  _stateMachine.Enter<GameState>(); break;
                case Const.Scene.Welcome:  _stateMachine.Enter<WelcomeState>(); break;
            }
        }

        private IEnumerator LoadScene()
        {
            _curtain.Show();
            yield return new WaitForSeconds(1f);
            _sceneLoader.Load(_sceneName, OnLoaded);
        }
    }
}
