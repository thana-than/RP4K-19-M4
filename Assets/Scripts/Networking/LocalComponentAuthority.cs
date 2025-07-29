using UnityEngine;
using Unity.Netcode;
using Horror.Utilities;

namespace Horror
{
    public class LocalComponentAuthority : NetworkBehaviour
    {
        [SerializeField] MonoBehaviour[] components;

        public override void OnNetworkSpawn()
        {
            bool enabled = this.CanControl();
            foreach (MonoBehaviour component in components)
            {
                component.enabled = enabled;
            }
        }
    }
}
