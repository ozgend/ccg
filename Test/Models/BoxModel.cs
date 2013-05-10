using denolk.CCG.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Models
{
    [GenerateClientModel(1.0, GenerateClientModelAttribute.Target.Java, GenerateClientModelAttribute.Target.ObjectiveC)]
    public class BoxModel
    {
        public long DateTicks { get; set; }
        public string OwnerName { get; set; }
        public string OwnerDepartment { get; set; }
        public string OwnerEmail { get; set; }
        public int DataId { get; set; }
        public int StepdataId { get; set; }
        public string DirectLink { get; set; }
        public string FlowCode { get; set; }
        public string FlowName { get; set; }
        public int FlowNo { get; set; }
        public string Summary { get; set; }
        public bool AllowMultiple { get; set; }
    }
}
