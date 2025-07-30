using UnityEngine;

namespace Horror.StateMachine
{
    public class StateMachine<T>
    {
        IState<T> defaultState;
        public IState<T> CurrentState { get; protected set; }

        public StateMachine(IState<T> defaultState)
        {
            this.defaultState = defaultState;
        }

        public void Restart(T payload)
        {
            CurrentState = defaultState;
            CurrentState.EnterState(payload);
        }

        public void Update(T payload)
        {
            IState<T> nextState = CurrentState.Update(payload);
            if (nextState != CurrentState)
            {
                CurrentState.ExitState(payload);
                CurrentState = nextState;
                CurrentState.EnterState(payload);
            }
        }
    }
}
