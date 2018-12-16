using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitGenerator
{
    class FullAdder : CompositeComponent
    {
        private Component xor1, xor2, and1, and2, or;        
        public FullAdder()
        {
            this.xor1 = ComponentFactory.GenerateXorGate();
            this.xor2 = ComponentFactory.GenerateXorGate();
            this.and1 = ComponentFactory.GenerateAndGate();
            this.and2 = ComponentFactory.GenerateAndGate();
            this.or = ComponentFactory.GenerateOrGate();

            this.xor2.connect(0, this.xor1.outputs[0]);
            this.and1.connect(0, this.xor1.outputs[0]);
            this.or.connect(0, this.and1.outputs[0]);
            this.or.connect(1, this.and2.outputs[0]);            

            this.outputs = new OutputPin[2];
            this.outputs[0] = this.xor2.outputs[0];
            this.outputs[1] = this.or.outputs[0];
        }

        public void connect(int pinID, OutputPin otherPin)
        {
            if (pinID < 0 || pinID > 2)
            {
                throw new ArgumentException("pinID should be between 0 and 2");
            }
            if (pinID == 0)
            {
                this.xor1.connect(0, otherPin);
                this.and2.connect(0, otherPin);
            } else if(pinID == 1)
            {
                this.xor1.connect(1, otherPin);
                this.and2.connect(1, otherPin);
            } else
            {
                this.xor2.connect(1, otherPin);
                this.and1.connect(1, otherPin);
            }
        }

        public override Component[] getChildren()
        {
            Component[] components = new Component[5]
            {
                xor1, xor2, and1, and2, or
            };

            return components;
        }
    }
}
