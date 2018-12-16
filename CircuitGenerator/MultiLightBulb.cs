using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitGenerator
{
    class MultiLightBulb : CompositeComponent
    {
        private Component[] lightBulbs;

        public MultiLightBulb(int numLightBulbs)
        {
            if (numLightBulbs <= 0)
            {
                throw new ArgumentException("Number of light bulbs should be positive");
            }

            this.lightBulbs = new Component[numLightBulbs];
            for (int i = 0; i < numLightBulbs; i++)
            {
                this.lightBulbs[i] = ComponentFactory.GenerateLightBulb();
            }
            this.outputs = new OutputPin[0];

        }

        public MultiLightBulb(int numLightBulbs, string label) : this(numLightBulbs)
        {
            this.addLabel(label);
        }

        public override Component[] getChildren()
        {
            return this.lightBulbs;
        }

        public void connect(OutputPin[] inputs)
        {
            if (this.lightBulbs.Length != inputs.Length)
            {
                throw new ArgumentException("The number of inputs should equal the number of light bulbs");
            }

            for (int i = 0; i < inputs.Length; i++)
            {
                this.lightBulbs[i].connect(0, inputs[i]);
            }
        }

        public void addLabel(string label)
        {
            for (int i = 0; i < lightBulbs.Length; i++)
            {
                lightBulbs[i].addLabel(label + i);
            }
        }
    }
}
