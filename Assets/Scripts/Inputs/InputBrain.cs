using UnityEngine;

namespace Horror.Inputs
{
    public abstract class InputBrain : MonoBehaviour
    {
        public abstract Vector3 Movement { get; }
        public abstract Vector2 Look { get; }
        public abstract bool JumpHeld { get; }
    }
}
