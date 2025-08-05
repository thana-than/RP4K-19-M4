using UnityEngine;
using UnityEngine.AI;
using Horror.Utilities;

namespace Horror.StateMachine
{
    [System.Serializable]
    public class PlayerSearcher
    {
        [SerializeField] float maxDistance = 20f;
        private enum SearchType
        {
            Random,
            Closest,
            Sightline
        }
        [SerializeField] SearchType searchType;
        NavMeshPath workingPath = new NavMeshPath();

        public bool Search(GhostPayload payload, out GameObject player)
        {
            player = null;
            bool success = false;
            switch (searchType)
            {
                case SearchType.Random:
                    success = RandomSearch(payload, out player);
                    break;
                case SearchType.Closest:
                    success = ClosestSearch(payload, out player);
                    break;
                case SearchType.Sightline:
                    success = SightlineSearch(payload, out player);
                    break;
            }

            return success;
        }

        public bool IsTargetValid(NavMeshAgent agent, Transform target)
        {
            bool pathFound = agent.CalculatePath(target.position, workingPath);
            if (!pathFound || workingPath.status != NavMeshPathStatus.PathComplete)
                return false;

            float distance = workingPath.CalculateDistance();
            if (distance > maxDistance)
                return false;

            return true;
        }

        bool RandomSearch(GhostPayload payload, out GameObject player)
        {
            player = PlayerManager.Instance.RandomPlayer();
            if (!IsTargetValid(payload.Agent, player.transform))
                player = null;

            return player != null;
        }

        bool ClosestSearch(GhostPayload payload, out GameObject player)
        {
            player = PlayerManager.Instance.ClosestPlayer(payload.Agent);
            return player != null;
        }

        bool SightlineSearch(GhostPayload payload, out GameObject player)
        {
            throw new System.NotImplementedException("SightlineSearch not implemented yet.");
        }
    }
}
