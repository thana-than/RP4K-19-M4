using UnityEngine;
using Horror.Inputs;

namespace Horror.Controllers
{
    public abstract class InputControllerBase : MonoBehaviour
    {
        [SerializeField] protected InputBrain inputBrain;

        protected virtual void Reset()
        {
            Init();
        }

        protected virtual void OnValidate()
        {
            Init();
        }

        protected virtual void Awake()
        {
            Init();
        }

        void Init()
        {
            if (inputBrain == null)
            {
                inputBrain = GetComponentInParent<InputBrain>(includeInactive: true);
            }
        }
    }
}
