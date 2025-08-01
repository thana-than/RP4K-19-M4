using UnityEngine;

namespace Horror
{
    public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
    {
        public static T Instance;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = (T)this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
