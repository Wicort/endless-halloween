using Logic;
using UnityEngine;

namespace Infrastructure.SatateMachine.States
{
    public class GameState : IState
    {
        private GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;

        public GameState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
        }

        public void Enter()
        {
            Debug.Log("Game state started");
        }

        public void Exit()
        {
            
        }
    }
}
