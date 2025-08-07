using Unity.Netcode;
using UnityEngine;

namespace Horror
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private int ghostIndex = -1;
        [SerializeField] private GameObject[] sources;
        System.Random random = new System.Random();
        public void Spawn()
        {
            GameObject source;
            if (ghostIndex >= 0 && ghostIndex < sources.Length)
                source = sources[ghostIndex];
            else
                source = sources[random.Next(sources.Length)];

            NetworkObject clone = NetworkObject.InstantiateAndSpawn(source, NetworkManager.Singleton, position: transform.position, rotation: transform.rotation);
        }
    }
}
