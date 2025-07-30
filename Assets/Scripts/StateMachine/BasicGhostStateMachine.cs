using Horror.Inputs;
using UnityEngine;

namespace Horror.StateMachine
{
    #region StateMachine
    public class BasicGhostStateMachine : InputBrain
    {
        private StateMachine<Payload> stateMachine = new StateMachine<Payload>(new IdleState());
        [SerializeField] float radius = 1f;

        InputValues inputValues = new InputValues();

        protected override InputValues InternalInput => inputValues;

        Payload GetPayload()
        {
            Payload payload = new Payload();
            payload.Self = this.transform;
            payload.Target = FindTarget();
            payload.Radius = radius;
            payload.inputValues = inputValues;

            return payload;
        }

        Transform FindTarget()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            float closestPlayerDistance = Mathf.Infinity;
            Transform closestPlayer = null;
            foreach (GameObject player in players)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance < closestPlayerDistance)
                {
                    closestPlayerDistance = distance;
                    closestPlayer = player.transform;
                }
            }

            return closestPlayer;
        }

        void Start()
        {
            stateMachine.Restart(GetPayload());
        }

        void Update()
        {
            stateMachine.Update(GetPayload());
        }
    }
    #endregion

    #region States
    // * 2 States: Jumping when player is close, no jump when player isn't

    //* Jump: when player is close: provide jump input, switch to idle when player is away

    //* Idle: wait for player to get close, then switch to jump state

    public struct Payload
    {
        public Transform Self, Target;
        public float Radius;
        public InputValues inputValues;
    }

    class JumpState : IState<Payload>
    {
        public void EnterState(Payload payload) { }

        public void ExitState(Payload payload) { }

        public IState<Payload> Update(Payload payload)
        {
            payload.inputValues.JumpHeld = true;

            if (payload.Target == null)
                return new IdleState();

            if (Vector3.Distance(payload.Target.position, payload.Self.position) >= payload.Radius)
                return new IdleState();

            return this;
        }
    }

    class IdleState : IState<Payload>
    {
        public void EnterState(Payload payload) { }
        public void ExitState(Payload payload) { }

        public IState<Payload> Update(Payload payload)
        {
            payload.inputValues.JumpHeld = false;

            if (payload.Target == null)
                return this;

            if (Vector3.Distance(payload.Target.position, payload.Self.position) < payload.Radius)
                return new JumpState();

            return this;
        }
    }

    #endregion
}
