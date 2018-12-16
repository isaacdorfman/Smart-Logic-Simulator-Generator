using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitGenerator
{
    class UnsignedAdder : CompositeComponent
    {
        public int numBits;
        private FullAdder[] fullAdders;
        private int numInputs;
        private int numOutputs;
        
        public UnsignedAdder(int numBits)
        {
            if (numBits <= 0)
            {
                throw new ArgumentException("Number of bits should be at least 1");
            }

            this.numBits = numBits;
            this.numInputs = numBits * 2;
            this.numOutputs = numBits + 1;

            outputs = new OutputPin[numBits + 1];

            this.fullAdders = new FullAdder[numBits];
            for (int i = 0; i < numBits; i++)
            {
                this.fullAdders[i] = new FullAdder();
                this.outputs[i] = fullAdders[i].outputs[0];
                if (i != 0)
                {
                    this.fullAdders[i].connect(2, this.fullAdders[i - 1].outputs[1]);
                }
            }
            this.outputs[numBits] = this.fullAdders[numBits - 1].outputs[1];
        }        

        public void connectA(OutputPin[] pins)
        {
            if (pins.Length != numBits)
            {
                throw new ArgumentException("pins.Length should equal numBits");
            }
            for (int i = 0; i < numBits; i++)
            {
                this.fullAdders[i].connect(0, pins[i]);
            }
        }

        public void connectB(OutputPin[] pins)
        {
            if (pins.Length != numBits)
            {
                throw new ArgumentException("pins.Length should equal numBits");
            }
            for (int i = 0; i < numBits; i++)
            {
                this.fullAdders[i].connect(1, pins[i]);
            }
        }

        public override Component[] getChildren()
        {
            return fullAdders.Select(adder => adder.getChildren()).SelectMany(i => i).ToArray();
        }
    }
}
