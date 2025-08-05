using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Horror.Utilities;

namespace Horror
{
    public class PlayerManager : SingletonBehaviour<PlayerManager>
    {
        public List<GameObject> Players { get; private set; } = new List<GameObject>();

        [SerializeField] float checkTimer = 30.0f;
        float current_checkTimer = 0.0f;

        void Update()
        {
            if (current_checkTimer >= checkTimer)
                PollForPlayers();

            current_checkTimer += Time.deltaTime;
        }

        void PollForPlayers()
        {
            current_checkTimer = 0;

            var players = GameObject.FindGameObjectsWithTag("Player").Where(p => !Players.Contains(p));
            Players.AddRange(players);
        }

        public GameObject RandomPlayer()
        {
            int count = Players.Count;
            if (count == 0)
                return null;

            int randomIndex = Random.Range(0, count);
            return Players[randomIndex];
        }

        public GameObject ClosestPlayer(NavMeshAgent agent)
        {
            GameObject closestPlayer = null;
            float closestDistance = Mathf.Infinity;
            NavMeshPath workingPath = new NavMeshPath();

            foreach (GameObject player in Players)
            {
                bool pathFound = agent.CalculatePath(player.transform.position, workingPath);
                if (!pathFound || workingPath.status != NavMeshPathStatus.PathComplete)
                    continue;

                float distance = workingPath.CalculateDistance();
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPlayer = player;
                }
            }

            return closestPlayer;
        }
    }
}
