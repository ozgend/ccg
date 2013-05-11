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

            creator.Create();

            creator.Create("Test.Models");

            creator.Create("Test", true);
        }
    }
}
