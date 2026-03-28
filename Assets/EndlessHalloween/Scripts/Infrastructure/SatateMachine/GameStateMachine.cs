using DIContainer;
using Infrastructure.SatateMachine.States;
using Logic;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.SatateMachine
{
    public class GameStateMachine
    {
        private List<IExitableState> _states;
        private IExitableState _currentState;
        private SceneLoader _sceneLoader;
        private LoadingCurtain _curtain;
        private DI _container;

        public IExitableState CurrentState => _currentState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, DI container)
        {
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _container = container;

            _states = new List<IExitableState>()
            {
                new BootstrapState(this, _sceneLoader, _container),
                new LoadLevelState(this, _sceneLoader, _curtain),
                new WelcomeState(this, _sceneLoader, _curtain),
                new GameState(this, _sceneLoader, _curtain),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();

            TState state = GetState<TState>();
            _currentState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states.FirstOrDefault(state => state is TState) as TState;
        }
    }
}