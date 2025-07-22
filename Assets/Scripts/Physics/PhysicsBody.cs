using UnityEngine;

namespace Horror.Physics
{
    public class PhysicsBody : MonoBehaviour
    {
        //* GOALS:
        //? Gravity
        //? Velocity
        //? Collision?
        //! Doesn't handle input, just physics

        [SerializeField] private CharacterController controller;
        [SerializeField] float gravity = 1.0f;
        [SerializeField] float drag = 0.1f;
        private Vector3 externalForces;
        private Vector3 velocity;
        public bool isGrounded => controller.isGrounded;

        public void AddForce(Vector3 force, ForceMode mode = ForceMode.Force)
        {
            if (mode == ForceMode.Force || mode == ForceMode.Acceleration)
            {
                force *= Time.fixedDeltaTime;
            }
            else
            {
                force /= Time.fixedDeltaTime;
            }

            externalForces += force;
        }

        void FixedUpdate()
        {
            Vector3 moveStep = Vector3.zero;
            velocity = ApplyGravity(velocity);
            velocity = ApplyForce(velocity);
            velocity = ApplyCollision(velocity);

            moveStep = SetSpeed(moveStep);
            controller.Move(moveStep);
        }

        Vector3 ApplyGravity(Vector3 velocity)
        {
            velocity += UnityEngine.Physics.gravity * gravity * Time.fixedDeltaTime;
            return velocity;
        }

        Vector3 ApplyForce(Vector3 velocity)
        {
            velocity += externalForces;
            externalForces = Vector3.zero;

            return velocity;
        }

        Vector3 SetSpeed(Vector3 moveStep)
        {
            moveStep += velocity * Time.fixedDeltaTime;
            velocity *= 1f - drag * Time.fixedDeltaTime;
            return moveStep;
        }

        Vector3 ApplyCollision(Vector3 velocity)
        {
            if (controller.isGrounded)
            {
                velocity.y = 0f;
            }

            return velocity;
        }
    }
}
