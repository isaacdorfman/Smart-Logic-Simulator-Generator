using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitGenerator
{
    class MultiBitAnd : CompositeComponent
    {
        private Component[] andGates;        
        int inputLength;

        public MultiBitAnd(int inputLength)
        {
            if (inputLength <= 0)
            {
                throw new Exception("MultiBitAnd input length should be positive");
            }

            this.inputLength = inputLength;

            andGates = new Component[inputLength];
            for (int i = 0; i < inputLength; i++)
            {
                andGates[i] = ComponentFactory.GenerateAndGate();
            }

            outputs = andGates.Select(gate => gate.outputs[0]).ToArray();
        }        

        public void connectBit(OutputPin bit)
        {
            foreach (var andGate in andGates)
            {
                andGate.connect(0, bit);
            }
        }

        public void connectInput(OutputPin[] inputs)
        {
            if (inputs.Length != this.inputLength)
            {
                throw new Exception("Input length should be same as the Component's input length");
            }

            for (int i = 0; i < this.inputLength; i++)
            {
                andGates[i].connect(1, inputs[i]);
            }
        }

        public override Component[] getChildren()
        {
            return this.andGates;
        }
    }
}
