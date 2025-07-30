using UnityEngine;

namespace Horror.Inputs
{
    public class ExposedBrain : InputBrain
    {
        public InputValues input = new InputValues();
        protected override InputValues InternalInput => input;
    }
}
