using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitGenerator
{    
    class IC
    {
        Component[] components;
        public JObject generate()
        {
            dynamic json = new JObject();
            dynamic header = new JObject();
            header.TAG = "IC";
            header.APP_VERSION = 64;
            json.HEADER = header;

            dynamic jsonComponents = new JArray();
            foreach (Component component in components)
            {
                jsonComponents.Add(component.ExportAsIC());
            }

            json.COMPONENTS = jsonComponents;
            return json;
        }

        public IC(Component[] components)
        {
            this.components = components;
        }
    }
}
