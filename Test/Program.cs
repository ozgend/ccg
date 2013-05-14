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
            ClientCodeGenerator generator = new ClientCodeGenerator();

            var dict1 = generator.ToDictionary();

            var dict2 = generator.ToDictionary("Test.Models");

            var dict3 = generator.ToDictionary("Test", true);
        }
    }
}
