using Horror.Inputs;
using UnityEngine;
using UnityEngine.AI;

namespace Horror.StateMachine
{
    public class GhostPayload
    {
        public InputValues InputValues;
        public Transform Transform;
        public Transform Target;
        public NavMeshAgent Agent;
        public GhostBrain Brain;

        public int PatrolDestinationIndex = -1;
    }
}
