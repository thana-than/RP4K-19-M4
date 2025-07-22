using UnityEngine;

namespace Horror.Inputs
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private float speed = 5f;
        private Vector3 inputBuffer;

        void Update()
        {
            inputBuffer = GetInput();
        }

        void FixedUpdate()
        {
            controller.Move(inputBuffer * Time.fixedDeltaTime * speed);
        }

        Vector3 GetInput()
        {
            Vector2 rawInput = new Vector2(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical")
            );

            Vector2 input = Vector2.ClampMagnitude(rawInput, 1f);

            return new Vector3(
                input.x,
                0f,
                input.y
            );
        }
    }
}