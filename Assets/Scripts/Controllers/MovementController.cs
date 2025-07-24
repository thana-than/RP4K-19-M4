using UnityEngine;
using Horror.Physics;
using Horror.Inputs;

namespace Horror.Controllers
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private PhysicsBody physicsBody;
        [SerializeField] private InputBrain inputBrain;
        [SerializeField] private float speed = 5f;

        void FixedUpdate()
        {
            Vector3 right = transform.right * inputBrain.Movement.x;
            Vector3 up = transform.up * inputBrain.Movement.y;
            Vector3 forward = transform.forward * inputBrain.Movement.z;

            Vector3 movement = right + up + forward;

            physicsBody.Move(movement * Time.fixedDeltaTime * speed);
        }
    }
}