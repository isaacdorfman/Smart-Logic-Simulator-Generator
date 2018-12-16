using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    class UUID
    {
        private static int ID = 10000000;
        public static int getUUID()
        {
            return ID++;
            
        }
    }
}
