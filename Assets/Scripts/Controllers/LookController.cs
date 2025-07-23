using UnityEngine;
using Horror.Inputs;

namespace Horror.Controllers
{
    public class LookController : MonoBehaviour
    {
        [SerializeField] private InputBrain inputBrain;
        [SerializeField] private Transform head;

        void Update()
        {
            if (inputBrain.Look.x != 0)
                transform.Rotate(Vector3.up, inputBrain.Look.x);

            if (inputBrain.Look.y != 0)
                head.Rotate(Vector3.right, -inputBrain.Look.y);
        }
    }
}
