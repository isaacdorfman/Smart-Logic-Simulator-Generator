using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitGenerator
{
    static class ComponentFactory
    {
        public static Component GenerateAndGate(float X = 0, float Y = 0)
        {
            return new Component(2, 1, "AND_GATE", X, Y);
        }

        public static Component GenerateOrGate(float X = 0, float Y = 0)
        {
            return new Component(2, 1, "OR_GATE", X, Y);
        }

        public static Component GenerateXorGate(float X = 0, float Y = 0)
        {
            return new Component(2, 1, "XOR_GATE", X, Y);
        }

        public static Component GenerateNotGate(float X = 0, float Y = 0)
        {
            return new Component(1, 1, "NOT_GATE", X, Y);
        }

        public static Component GenerateLowConstant(float X = 0, float Y = 0)
        {
            return new Component(0, 1, "LOW_CONSTANT", X, Y);
        }

        public static Component GenerateHighConstant(float X = 0, float Y = 0)
        {
            return new Component(0, 1, "HIGH_CONSTANT", X, Y);
        }

        public static Component GeneratePulseButton(float X = 0, float Y = 0)
        {
            return new Component(0, 1, "PULSE_BUTTON", X, Y);
        }

        public static Component GenerateToggleButton(float X = 0, float Y = 0)
        {
            return new Component(0, 1, "TOGGLE_BUTTON", X, Y);
        }

        public static Component GenerateLightBulb(float X = 0, float Y = 0)
        {
            return new Component(1, 0, "LIGHT_BULB", X, Y);
        }

        public static Component GenerateSRLatch(float X = 0, float Y = 0)
        {
            return new Component(2, 2, "SR_LATCH", X, Y);
        }
    }
}
