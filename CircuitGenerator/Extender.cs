using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitGenerator
{
    class Extender : CompositeComponent
    {
        private Component[] lowConstants;

        public Extender(int size, OutputPin[] inputs, int offset = 0)
        {
            if (inputs.Length + offset > size)
            {
                throw new ArgumentException("inputs.Length + offset > size");
            }

            outputs = new OutputPin[size];

            lowConstants = new Component[size - inputs.Length];

            for (int i = 0; i < offset; i++)
            {
                lowConstants[i] = ComponentFactory.GenerateLowConstant();
                outputs[i] = lowConstants[i].outputs[0];
            }

            for (int i = 0; i < inputs.Length; i++)
            {
                outputs[offset + i] = inputs[i];
            }

            for (int i = 0; i < size - (offset + inputs.Length); i++)
            {
                Component lowConstant = ComponentFactory.GenerateLowConstant();
                lowConstants[offset + i] = lowConstant;
                outputs[offset + inputs.Length + i] = lowConstant.outputs[0];
            }
        }

        public override Component[] getChildren()
        {
            return lowConstants;
        }
    }
}
