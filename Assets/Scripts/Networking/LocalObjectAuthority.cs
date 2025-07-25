using Unity.Netcode;

namespace Horror
{
    public class LocalObjectAuthority : NetworkBehaviour
    {
        void Start()
        {
            if (!IsSpawned || !IsOwner)
                gameObject.SetActive(false);
        }
    }
}
