using Horror.Physics;
using UnityEngine;

namespace Horror.Inputs
{
    public class JumpController : MonoBehaviour
    {
        [SerializeField] private PhysicsBody physicsBody;
        [SerializeField] private float jumpForce = 5f;

        void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                AttemptJump();
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
