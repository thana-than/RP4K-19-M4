using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Horror
{
    public class PlayerManager : MonoBehaviour
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
    }
}
