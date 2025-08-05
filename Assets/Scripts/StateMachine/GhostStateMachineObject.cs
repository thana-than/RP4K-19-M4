using Horror.Utilities;
using UnityEngine;
using UnityEngine.AI;

namespace Horror.StateMachine
{
    [CreateAssetMenu(fileName = "GhostStateMachineObject", menuName = "State Machines/GhostStateMachineObject")]
    public class GhostStateMachineObject : GhostStateMachineObjectBase
    {
        [System.Serializable]
        class Settings
        {
            public float AggroRadius = 5f;
            public float PatrolStopBuffer = .15f;
            public float PlayerSearchInterval = 3f;
            public float PlayerValidCheckInterval = 1f;
            public PlayerSearcher Searcher;
        }

        [SerializeField] Settings settings;
        public override StateMachine<GhostPayload> InstantiateStateMachine() => new StateMachine<GhostPayload>(new PatrolState(settings));

        class ChaseState : GhostState<Settings>
        {

            float timeSinceLastValidCheck = 0f;
            public ChaseState(Settings settings) : base(settings) { }

            public override void EnterState(GhostPayload payload)
            {
                timeSinceLastValidCheck = 0f;
            }
            public override IState<GhostPayload> Update(GhostPayload payload)
            {
                if (payload.Target == null)
                    return new PatrolState(settings);

                if (timeSinceLastValidCheck > settings.PlayerValidCheckInterval)
                {
                    timeSinceLastValidCheck = 0f;
                    if (!settings.Searcher.IsTargetValid(payload.Agent, payload.Target))
                        return new PatrolState(settings);
                }
                timeSinceLastValidCheck += Time.deltaTime;

                payload.Brain.MoveTowards(payload.Target.position);

                return this;
            }
        }

        class IdleState : GhostState<Settings>
        {
            public IdleState(Settings settings) : base(settings) { }
            public override IState<GhostPayload> Update(GhostPayload payload)
            {
                payload.InputValues.JumpHeld = false;

                if (payload.Target == null)
                    return new PatrolState(settings);

                if (Vector3.Distance(payload.Target.position, payload.Transform.position) < settings.AggroRadius)
                    return new ChaseState(settings);

                return this;
            }
        }

        class PatrolState : GhostState<Settings>
        {
            float timeSinceLastSearch = 0f;
            public PatrolState(Settings settings) : base(settings) { }

            public override void EnterState(GhostPayload payload)
            {
                payload.Target = null;

                if (payload.PatrolDestinationIndex < 0)
                    payload.PatrolDestinationIndex = 0;

                timeSinceLastSearch = 0f;
            }

            public override IState<GhostPayload> Update(GhostPayload payload)
            {
                Vector3[] patrolPoints = PatrolPointManager.Instance.Points;
                int index = payload.PatrolDestinationIndex;
                float stopBuffer = settings.PatrolStopBuffer;

                payload.Brain.MoveTowards(patrolPoints[index]);

                if (payload.Agent.remainingDistance <= stopBuffer)
                    payload.PatrolDestinationIndex = (index + 1) % patrolPoints.Length;

                timeSinceLastSearch += Time.deltaTime;
                if (timeSinceLastSearch >= settings.PlayerSearchInterval)
                {
                    timeSinceLastSearch = 0f;
                    bool foundPlayer = settings.Searcher.Search(payload, out GameObject player);
                    if (foundPlayer)
                    {
                        payload.Target = player.transform;
                        return new ChaseState(settings);
                    }
                }

                return this;
            }
        }
    }
}
