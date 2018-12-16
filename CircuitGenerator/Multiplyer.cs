using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitGenerator
{
    class Multiplyer : CompositeComponent
    {
        private MultiBitAnd[] firstAnders;
        private Extender[] extenders;
        private UnsignedAdder[] tempAdders;        
        private int n, m;

        //TODO: handle cases where n,m < 2
        public Multiplyer(OutputPin[] a, OutputPin[] b)
        {
            int n = a.Length, m = b.Length;
            this.n = n; this.m = m;
            if(n <= 0 || m <= 0)
            {
                throw new ArgumentException("Size of a and b should be positive");
            }

            if(n > 60 || m > 60)
            {
                throw new ArgumentException("Size of a and b should be lesser or equal to 60");
            }

            if(m == 1)
            {                
                this.firstAnders = new MultiBitAnd[1];
                this.firstAnders[0] = new MultiBitAnd(n);
                this.firstAnders[0].connectBit(b[0]);
                this.firstAnders[0].connectInput(a);
                this.outputs = this.firstAnders[0].outputs;
                return;
            }

            firstAnders = new MultiBitAnd[m];
            for (int i = 0; i < m; i++)
            {
                firstAnders[i] = new MultiBitAnd(n);
                firstAnders[i].connectBit(b[i]);
                firstAnders[i].connectInput(a);
            }

            extenders = new Extender[m];
            for (int i = 0; i < m; i++)
            {
                extenders[i] = new Extender(n + m, firstAnders[i].outputs, i);
            }

            tempAdders = new UnsignedAdder[m - 1];
            for (int i = 0; i < m-1; i++)
            {
                tempAdders[i] = new UnsignedAdder(n + m);
            }

            tempAdders[0].connectA(extenders[0].outputs);
            tempAdders[0].connectB(extenders[1].outputs);
            for (int i = 1; i < m-1; i++)
            {
                tempAdders[i].connectA(tempAdders[i - 1].outputs.Take(n + m).ToArray());
                tempAdders[i].connectB(extenders[i + 1].outputs);
            }

            this.outputs = tempAdders[m - 2].outputs.Take(n + m).ToArray();
        }

        public override Component[] getChildren()
        {
            if (m == 1)
            {
                return firstAnders[0].getChildren();
            }
            return new CompositeComponent[][] { firstAnders, extenders, tempAdders }.SelectMany(i => i).
                SelectMany(compositeComponent => compositeComponent.getChildren()).ToArray();
        }
    }
}
