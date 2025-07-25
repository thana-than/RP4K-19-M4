using UnityEngine;
using Horror.Inputs;

namespace Horror.Controllers
{
    public class LookController : MonoBehaviour
    {
        [SerializeField] private InputBrain inputBrain;
        [SerializeField] private Transform head;
        [SerializeField] private float maxVerticleAngle = 85f;

        private float headRotation = 0f;

        void Update()
        {
            if (inputBrain.Input.Look.x != 0)
                transform.Rotate(Vector3.up, inputBrain.Input.Look.x);

            if (inputBrain.Input.Look.y != 0)
            {
                headRotation = Mathf.Clamp(headRotation - inputBrain.Input.Look.y, -maxVerticleAngle, maxVerticleAngle);
                head.localEulerAngles = new Vector3(headRotation, head.localEulerAngles.y, head.localEulerAngles.z);
            }
        }
    }
}
