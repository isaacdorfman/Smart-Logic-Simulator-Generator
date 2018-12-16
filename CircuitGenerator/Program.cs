using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            const int registerSize = 3;

            MultiToggleButton input = new MultiToggleButton(registerSize, "input");
            Component write = ComponentFactory.GenerateToggleButton();
            write.addLabel("write");
            MultiLightBulb output = new MultiLightBulb(registerSize, "out");

            Register register = new Register(registerSize, input.getChildren().Select(x => x.outputs[0]).ToArray(), write.outputs[0]);
            output.connect(register.outputs);

            Component[] components = new CompositeComponent[] { input, register, output }.Select(compositeComponent => compositeComponent.getChildren()).SelectMany(i => i).Concat(new[] { write }).ToArray();
            
            IC ic = new IC(components);
            Console.WriteLine(ic.generate().ToString());
            Console.WriteLine("**********************");
            Console.WriteLine("Write the file name to save");
            string filename = Console.ReadLine();
            File.WriteAllText(@"C:\Users\isaac\Desktop\smartlogic\" + filename, ic.generate().ToString());
            Console.WriteLine("Finished saving circuit to file");
            Console.ReadLine();
        }
    }
}
