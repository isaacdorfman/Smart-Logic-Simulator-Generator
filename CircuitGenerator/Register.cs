using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitGenerator
{
    class Register : CompositeComponent
    {
        private Component[] srLatches;
        private Component[] notInputs;
        private Component[] inputAndWrites;
        private Component[] notInputAndWrites;

        private delegate Component ComponentGenerator();

        public Register(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentException("Register size should be positive");
            }

            srLatches = generateComponentArray(() => ComponentFactory.GenerateSRLatch(), size);
            notInputs = generateComponentArray(() => ComponentFactory.GenerateNotGate(), size);
            inputAndWrites = generateComponentArray(() => ComponentFactory.GenerateAndGate(), size);
            notInputAndWrites = generateComponentArray(() => ComponentFactory.GenerateAndGate(), size);

            for (int i = 0; i < size; i++)
            {
                notInputAndWrites[i].connect(0, notInputs[i].outputs[0]);
                srLatches[i].connect(0, inputAndWrites[i].outputs[0]);
                srLatches[i].connect(1, notInputAndWrites[i].outputs[0]);
            }

            outputs = srLatches.Select(latch => latch.outputs[0]).ToArray();
        }

        public Register(int size, OutputPin[] input, OutputPin write) : this(size)
        {
            this.connect(input, write);
        }

        public void connect(OutputPin[] input, OutputPin write)
        {
            if (input.Length != srLatches.Length)
            {
                throw new ArgumentException("Input size does not match Register size");
            }

            if (write == null)
            {
                throw new ArgumentNullException("write should be non null");
            }

            for (int i = 0; i < input.Length; i++)
            {
                inputAndWrites[i].connect(0, input[i]);
                inputAndWrites[i].connect(1, write);
                notInputs[i].connect(0, input[i]);
                notInputAndWrites[i].connect(1, write);
            }
        }

        private static Component[] generateComponentArray(ComponentGenerator generator, int length)
        {
            return Enumerable.Repeat(0, length).Select(i => generator()).ToArray();
        }

        public override Component[] getChildren()
        {
            return new Component[][] { srLatches, notInputs, inputAndWrites, notInputAndWrites }.SelectMany(i => i).ToArray();
        }
    }
}
