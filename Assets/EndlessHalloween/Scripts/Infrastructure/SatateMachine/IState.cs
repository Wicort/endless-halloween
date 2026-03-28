namespace Infrastructure.SatateMachine
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}
