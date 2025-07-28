using UnityEngine;

namespace Horror.Inputs
{
    public class ExposedBrain : InputBrain
    {
        public InputValues input;
        protected override InputValues InternalInput => input;
    }
}
