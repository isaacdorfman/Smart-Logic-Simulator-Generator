using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitGenerator
{
    public class ICArguments
    {
        public string uri;
        public int numInputs;
        public int numOutputs;
    }

    public class OutputPin
    {
        public int parent_ID;
        public int connector_ID;
        public OutputPin(int parent_ID, int connector_ID)
        {            
            this.parent_ID = parent_ID;
            this.connector_ID = connector_ID;
        }
    }
    public class InputPin
    {        
        public OutputPin input;
        public int parent_ID;
        public int connector_ID;
        public InputPin(int parent_ID, int connector_ID)
        {            
            this.parent_ID = parent_ID;
            this.connector_ID = connector_ID;
        }

        //Connect an output pin as an input to this pin
        public void Connect(OutputPin input)
        {
            this.input = input ?? throw new ArgumentNullException();
        }
    }

    class Component
    {
        public int ID;
        public int numInputs, numOutputs;
        private string tag;
        public float X, Y;
        public InputPin[] inputs;
        public OutputPin[] outputs;
        public string label = "";
        public ICArguments icArguments = null;

        public Component(int numInputs, int numOutputs, string tag, float X = 0, float Y = 0)
        {
            if(numInputs < 0 || numOutputs < 0)
            {
                throw new ArgumentException("Number of inputs and number outputs should be non negative");                
            }
            this.ID = Util.UUID.getUUID();
            this.X = X;
            this.Y = Y;
            this.numInputs = numInputs;
            this.numOutputs = numOutputs;
            this.tag = tag;

            this.inputs = new InputPin[numInputs];
            for (int i = 0; i < numInputs; i++)
            {
                inputs[i] = new InputPin(this.ID, i);
            }

            this.outputs = new OutputPin[numOutputs];
            for (int i = 0; i < numOutputs; i++)
            {
                outputs[i] = new OutputPin(this.ID, i);
            }
        }

        public void addLabel(string label)
        {
            this.label = label;
        }

        public void AddICArguments(ICArguments icArguments)
        {
            this.icArguments = icArguments;
        }

        public JObject ExportAsIC()
        {
            dynamic json = new JObject();
            json.ID = this.ID;
            json.TAG = tag;
            json.X = X;
            json.Y = Y;

            dynamic inputs = new JArray();

            for (int i = 0; i < this.inputs.Length; i++)
            {
                InputPin input = this.inputs[i];
                if (input.input == null)
                    continue;
                dynamic jsonInput = new JObject();
                jsonInput.CONNECTOR_ID = i;
                jsonInput.OTHER_COMPONENT = input.input.parent_ID;
                jsonInput.OTHER_CONNECTOR_ID = input.input.connector_ID;

                inputs.Add(jsonInput);
            }

            json.INPUTS = inputs;

            dynamic arguments = new JObject();
            bool isArgumentsEmpty = true;

            if(this.label != "")
            {
                arguments.LABEL = this.label;
                isArgumentsEmpty = false;
            }

            if(this.icArguments != null)
            {
                arguments.URI = this.icArguments.uri;
                arguments.NUM_OF_IN = this.icArguments.numInputs;
                arguments.NUM_OF_OUT = this.icArguments.numOutputs;
                isArgumentsEmpty = false;
            }

            if(!isArgumentsEmpty)
            {
                json.ARGUMENTS = arguments;
            }
            
            return json;
        }
        //public abstract JObject ExportAsProject();        
        
        public void connect(int inputPinID, OutputPin otherPin)
        {
            if (inputPinID < 0)
            {
                throw new ArgumentException("input_id must be non negative");
            }

            if(inputPinID >= numInputs)
            {
                throw new ArgumentException("input_id must be below numInputs");
            }

            this.inputs[inputPinID].Connect(otherPin);
        }        
    }
}
