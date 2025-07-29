using Horror.Utilities;
using Unity.Netcode;
using UnityEngine;

namespace Horror.Inputs
{
    public abstract class InputBrain : NetworkBehaviour
    {
        public bool AllowInput = true;

        public InputValues Input => InputAllowed ? InternalInput : InputValues.Empty;
        protected abstract InputValues InternalInput { get; }

        public bool InputAllowed => AllowInput && this.CanControl();
    }

    [System.Serializable]
    public struct InputValues
    {
        public Vector3 Movement;
        public Vector2 Look;
        public bool JumpHeld;

        public static readonly InputValues Empty = default(InputValues);
    }
}
