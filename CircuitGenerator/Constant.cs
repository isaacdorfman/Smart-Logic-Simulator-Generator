using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitGenerator
{
    class Constant : CompositeComponent
    {
        private Component[] constants;
        public Constant(long size, long value)
        {
            if (size <= 0 || size > 60)
            {
                throw new ArgumentException("Size should be between 1 and 60");
            }

            if (value < 0 || value >= Math.Pow(2, size))
            {
                throw new ArgumentException("value should be between 0 and 2^size - 1");
            }

            bool[] bitMask = new bool[size];
            for (int i = 0; i < size; i++)
            {
                bitMask[i] = value % 2 == 1;
                value /= 2;
            }
            this.constants = bitMask.Select(bit => bit ? ComponentFactory.GenerateHighConstant() : ComponentFactory.GenerateLowConstant()).ToArray();

            this.outputs = this.constants.Select(constant => constant.outputs[0]).ToArray();
        }

        public override Component[] getChildren()
        {
            return this.constants;
        }
    }
}
