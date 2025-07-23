using UnityEngine;

namespace Horror.Inputs
{
    public class PlayerBrain : InputBrain
    {
        private Vector3 movement;
        public override Vector3 Movement => movement;

        private bool jumpHeld;
        public override bool JumpHeld => jumpHeld;

        private Vector2 look;
        public override Vector2 Look => look;

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
            movement = GetInput();
            look = GetLook();
            jumpHeld = Input.GetButton("Jump");
        }

        Vector2 GetLook()
        {
            return new Vector2(
                Input.GetAxis("Mouse X"),
                Input.GetAxis("Mouse Y")
            ) * lookSensitivity;
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
