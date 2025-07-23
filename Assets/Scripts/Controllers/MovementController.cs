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
            physicsBody.Move(inputBrain.Movement * Time.fixedDeltaTime * speed);
        }
    }
}