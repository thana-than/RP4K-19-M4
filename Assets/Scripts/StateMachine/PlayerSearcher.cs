using UnityEngine;
using UnityEngine.AI;
using Horror.Utilities;
using System.Net;

namespace Horror.StateMachine
{
    [System.Serializable]
    public class PlayerSearcher
    {
        [SerializeField] float maxDistance = 20f;
        [SerializeField] float sightline_minRadius = .5f;
        [SerializeField] float sightline_maxRadius = 5f;
        [SerializeField] float sightline_radials = 3;
        [SerializeField] int sightline_rays = 20;
        private enum SearchType
        {
            Random,
            Closest,
            Sightline
        }
        [SerializeField] SearchType searchType;
        NavMeshPath _workingPath; //! Cannot be constructed yet, must be initialized later.
        NavMeshPath workingPath
        {
            get
            {
                if (_workingPath == null)
                    _workingPath = new NavMeshPath();

                return _workingPath;
            }
        }

        RaycastHit[] _raycastHits;
        RaycastHit[] raycastHits
        {
            get
            {
                if (_raycastHits == null || _raycastHits.Length != sightline_rays)
                    _raycastHits = new RaycastHit[sightline_rays];

                return _raycastHits;
            }
        }

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
            if (searchType == SearchType.Sightline)
            {
                if (UnityEngine.Physics.Linecast(agent.transform.position, target.transform.position, out RaycastHit hit))
                    return hit.distance < maxDistance && hit.collider.transform == target;

                return true;
            }

            float distance;

            if (agent.enabled)
            {
                bool pathFound = agent.CalculatePath(target.position, workingPath);
                if (!pathFound || workingPath.status != NavMeshPathStatus.PathComplete)
                    return false;

                distance = workingPath.CalculateDistance();
            }
            else
                distance = Vector3.Distance(agent.transform.position, target.position);

            if (distance > maxDistance)
                return false;

            return true;
        }

        bool RandomSearch(GhostPayload payload, out GameObject player)
        {
            player = PlayerManager.Instance.RandomPlayer();
            if (player != null && !IsTargetValid(payload.Agent, player.transform))
                player = null;

            return player != null;
        }

        GameObject GetFirstValidPlayer(NavMeshAgent agent, GameObject[] players)
        {
            for (int i = 0; i < players.Length; i++)
            {
                GameObject player = players[i];
                if (IsTargetValid(agent, player.transform))
                {
                    return player;
                }
            }

            return null;
        }

        bool ClosestSearch(GhostPayload payload, out GameObject player)
        {
            GameObject[] players = PlayerManager.Instance.ClosestPlayersSorted(payload.Agent);
            player = GetFirstValidPlayer(payload.Agent, players);

            return player != null;
        }

        bool SightlineSearch(GhostPayload payload, out GameObject player)
        {
            player = PlayerManager.Instance.SightlineRaycasts(payload.Transform, raycastHits, sightline_radials, sightline_minRadius, sightline_maxRadius, maxDistance);
            return player != null;
        }
    }
}
