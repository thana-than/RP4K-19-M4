using UnityEngine;

namespace Horror.Inputs
{
    public class PlayerBrain : InputBrain
    {
        private InputValues input;
        protected override InputValues InternalInput => input;

        [SerializeField] private float lookSensitivity = 1.0f;

        void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
        }

        void Update()
        {
            input.Movement = GetInput();
            input.Look = GetLook();
            input.JumpHeld = UnityEngine.Input.GetButton("Jump");
        }

        Vector2 GetLook()
        {
            return new Vector2(
                UnityEngine.Input.GetAxis("Mouse X"),
                UnityEngine.Input.GetAxis("Mouse Y")
            ) * lookSensitivity;
        }

        Vector3 GetInput()
        {
            Vector2 rawInput = new Vector2(
                UnityEngine.Input.GetAxis("Horizontal"),
                UnityEngine.Input.GetAxis("Vertical")
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
