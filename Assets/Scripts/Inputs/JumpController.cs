using Horror.Physics;
using UnityEngine;

namespace Horror.Inputs
{
    public class JumpController : MonoBehaviour
    {
        [SerializeField] private PhysicsBody physicsBody;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float jumpHoldGravityMod = 0.6f;

        bool jumpHeld = false;

        void Update()
        {
            jumpHeld = Input.GetButton("Jump");
        }

        void FixedUpdate()
        {
            if (jumpHeld)
            {
                AttemptJump();

                if (!physicsBody.isGrounded)
                    physicsBody.MultiplyGravity(jumpHoldGravityMod);
            }
        }

        void AttemptJump()
        {
            if (!physicsBody.isGrounded)
            {
                return;
            }
            physicsBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
