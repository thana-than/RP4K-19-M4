using Horror.Utilities;
using Unity.Netcode;
using UnityEngine;

namespace Horror
{
    public class LocalObjectAuthority : NetworkBehaviour
    {
        [SerializeField] GameObject[] targets;
        public override void OnNetworkSpawn()
        {
            foreach (var target in targets)
                target.SetActive(this.CanControl());
        }
    }
}
