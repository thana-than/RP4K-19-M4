using UnityEngine;

namespace Horror.StateMachine
{
    public abstract class StateMachineObject<T> : ScriptableObject
    {
        public abstract StateMachine<T> InstantiateStateMachine();
    }
}
