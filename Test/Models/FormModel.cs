using denolk.CCG.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Models
{
    [GenerateClientCode(1.0, GenerateClientCodeAttribute.Target.Java, GenerateClientCodeAttribute.Target.ObjectiveC)]
    public class FormModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Versions { get; set; }
    }
}
