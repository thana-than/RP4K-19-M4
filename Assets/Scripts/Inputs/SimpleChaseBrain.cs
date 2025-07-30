using UnityEngine;
using UnityEngine.AI;

namespace Horror.Inputs
{
    public class SimpleChaseBrain : InputBrain
    {
        InputValues input = new InputValues();
        protected override InputValues InternalInput => input;
        [SerializeField] Vector3 movementMultipliers = new Vector3(1f, 0f, 1f);
        [SerializeField] Vector3 lookMultipliers = new Vector3(1f, .5f, 1f);
        [SerializeField] float lookMaxSpeed = 100f;
        [SerializeField] bool canJump = true;
        [SerializeField] float jumpHeightThreshold = .6f;
        [SerializeField] NavMeshAgent agent;

        void OnValidate()
        {
            if (agent != null) return;

            agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            //* Try to find the target (player)
            //* If a target is found, move towards it
            //* If not, provide no input

            Transform target = FindTarget();

            if (!target)
            {
                input = InputValues.Empty;
                return;
            }

            agent.destination = target.position;
            MoveTowards(target);
            AttemptJump(target);
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

        void MoveTowards(Transform target)
        {
            Vector3 dir = agent.desiredVelocity;//target.position - transform.position;
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

        void AttemptJump(Transform target)
        {
            input.JumpHeld = canJump && target.position.y - transform.position.y > jumpHeightThreshold;
        }
    }
}
