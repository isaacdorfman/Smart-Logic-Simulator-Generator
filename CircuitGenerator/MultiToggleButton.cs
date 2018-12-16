using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitGenerator
{
    class MultiToggleButton : CompositeComponent
    {
        private Component[] toggleButtons;

        public MultiToggleButton(int numButtons)
        {
            if (numButtons <= 0)
            {
                throw new Exception("Number of toggle buttons should be positive");
            }

            toggleButtons = new Component[numButtons];
            for (int i = 0; i < numButtons; i++)
            {
                toggleButtons[i] = ComponentFactory.GenerateToggleButton();
            }

            this.outputs = toggleButtons.Select(button => button.outputs[0]).ToArray();
        }

        public MultiToggleButton(int numButtons, string label) : this(numButtons)
        {
            this.addLabel(label);
        }

        public override Component[] getChildren()
        {
            return toggleButtons;
        }

        public void addLabel(string label)
        {
            for (int i = 0; i < toggleButtons.Length; i++)
            {
                this.toggleButtons[i].addLabel(label + i);
            }
        }
    }
}
