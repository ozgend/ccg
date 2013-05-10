using denolk.CCG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Creator creator = new Creator();
            creator.Create(typeof(Program));
        }
    }
}
