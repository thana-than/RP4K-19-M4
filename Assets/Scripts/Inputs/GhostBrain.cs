using Horror.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Horror.Inputs
{
    public class GhostBrain : InputBrain
    {
        private InputValues input = new InputValues();
        protected override InputValues InternalInput => input;

        [SerializeField] GhostStateMachineObjectBase stateMachineObject;
        GhostPayload payload = new GhostPayload();

        StateMachine<GhostPayload> stateMachine;

        GhostStateMachineObjectBase cached_stateMachineObject;

        [SerializeField] NavMeshAgent agent;

        [SerializeField] Vector3 movementMultipliers = new Vector3(1f, 0f, 1f);
        [SerializeField] Vector3 lookMultipliers = new Vector3(1f, .5f, 1f);
        [SerializeField] float lookMaxSpeed = 100f;

        public void MoveTowardsDirection(Vector3 direction)
        {
            Vector3 dir = direction;//agent.desiredVelocity;//target.position - transform.position;
            Vector3 localDir = transform.InverseTransformDirection(dir);

            Vector3 moveDir = localDir;
            moveDir = Vector3.Scale(moveDir, movementMultipliers);
            moveDir = Vector3.ClampMagnitude(moveDir, 1f);

            Vector3 lookDir = localDir;
            lookDir = Vector3.Scale(lookDir, lookMultipliers);
            lookDir = Vector3.ClampMagnitude(lookDir, lookMaxSpeed);

            input.Movement = moveDir;
            input.Look = lookDir;
        }

        public void MoveTowards(Vector3 position, bool useNavMesh = true)
        {
            if (agent.enabled)
                agent.destination = position;
            MoveTowardsDirection(useNavMesh ? agent.desiredVelocity : position - transform.position);
        }

        void OnValidate()
        {
            if (Application.isPlaying && stateMachineObject != cached_stateMachineObject)
                GenerateStateMachine();

            if (!agent)
                agent = GetComponent<NavMeshAgent>();
        }

        void GenerateStateMachine()
        {
            if (!InputAllowed)
                return;

            FreshPayload();

            cached_stateMachineObject = stateMachineObject;
            stateMachine = stateMachineObject.InstantiateStateMachine();

            stateMachine.Restart(payload);
        }

        void FreshPayload()
        {
            payload = new GhostPayload();

            payload.Transform = transform;
            payload.InputValues = input;
            payload.Target = null;
            payload.Agent = agent;
            payload.Brain = this;
        }

        public override void OnNetworkSpawn()
        {
            GenerateStateMachine();
        }

        void Update()
        {
            if (InputAllowed && stateMachine != null)
                stateMachine.Update(payload);
        }
    }
}
