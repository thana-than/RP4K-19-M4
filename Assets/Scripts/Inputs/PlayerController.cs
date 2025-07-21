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
            return new Vector3(
                Input.GetAxis("Horizontal"),
                0f,
                Input.GetAxis("Vertical")
            );
        }
    }
}