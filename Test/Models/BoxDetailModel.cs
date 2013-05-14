using denolk.CCG.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Models
{
    [GenerateClientCode(GenerateClientCodeAttribute.Target.Java)]
    public class BoxDetailModel
    {
        public int DataId { get; set; }
        public int StepdataId { get; set; }
        public string FlowCode { get; set; }
        public string Html { get; set; }
        public string Text { get; set; }
        public int ReportId { get; set; }
        public string Reason { get; set; }
        public bool HasApprove { get; set; }
        public bool HasSendback { get; set; }
        public bool HasCancel { get; set; }
        public bool HasPool { get; set; }
        public bool IsInPool { get; set; }
    }
}
