using UnityEngine;
using Horror.Utilities;

namespace Horror.Physics
{
    public class PhysicsBody : NetworkAuthorizedBehaviour
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
        private Vector3 moveBuffer;
        private Vector3 velocity;
        public bool isGrounded => controller.isGrounded;

        public void AddForce(Vector3 force, ForceMode mode = ForceMode.Force)
        {
            if (mode == ForceMode.Force || mode == ForceMode.Acceleration)
            {
                force *= Time.fixedDeltaTime;
            }

            externalForces += force;
        }

        public void Move(Vector3 moveStep)
        {
            moveBuffer += moveStep;
        }

        void FixedUpdate()
        {
            velocity = ApplyGravity(velocity);
            velocity = ApplyForce(velocity);
            velocity = ApplyCollision(velocity);

            moveBuffer = SetSpeed(moveBuffer);
            controller.Move(moveBuffer);

            moveBuffer = Vector3.zero;
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
                velocity.y = Mathf.Max(-controller.skinWidth, velocity.y);
            }

            return velocity;
        }

        void OnDrawGizmosSelected()
        {
            controller.DrawGroundContactGizmos();
        }
    }
}
