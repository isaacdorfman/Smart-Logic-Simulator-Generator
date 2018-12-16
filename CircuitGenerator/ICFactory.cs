using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitGenerator
{
    static class ICFactory
    {
        public static IC GenerateUnsignedConstant(int numBits, int value)
        {
            if (numBits <= 0)
            {
                throw new ArgumentException("The number of bits has to be positive");
            }

            if (value < 0 || value >= Math.Pow(2, numBits))
            {
                throw new ArgumentException("The value has to be between 0 and 2^numBits");
            }

            bool[] bitMask = new bool[numBits];

            for (int i = numBits - 1; i >= 0; i--)
            {
                bitMask[i] = (value % 2) == 1;
                value /= 2;
            }

            Component[] constants = new Component[numBits];
            for (int i = 0; i < numBits; i++)
            {
                if (bitMask[i])
                {
                    constants[i] = ComponentFactory.GenerateHighConstant();
                }
                else
                {
                    constants[i] = ComponentFactory.GenerateLowConstant();
                }
            }

            Component[] lightBulbs = new Component[numBits];
            for (int i = 0; i < numBits; i++)
            {
                lightBulbs[i] = ComponentFactory.GenerateLightBulb();
                lightBulbs[i].addLabel("input" + i);
            }

            for (int i = 0; i < numBits; i++)
            {
                lightBulbs[i].connect(0, constants[i].outputs[0]);
            }

            return new IC(constants.Concat(lightBulbs).ToArray());
        }
    }
}
