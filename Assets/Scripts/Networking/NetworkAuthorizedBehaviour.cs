using UnityEngine;
using Unity.Netcode;

namespace Horror
{
    public abstract class NetworkAuthorizedBehaviour : NetworkBehaviour
    {
        protected virtual void Start()
        {
            if (!IsOwner)
                enabled = false;
        }
    }
}
