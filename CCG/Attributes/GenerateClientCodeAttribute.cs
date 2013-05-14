using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace denolk.CCG.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GenerateClientCodeAttribute : Attribute
    {
        public enum Target
        {
            Java, ObjectiveC
        }

        public double Version { get; set; }
        public Target[] Targets { get; set; }

        public GenerateClientCodeAttribute()
            : this(1.0, Target.Java, Target.ObjectiveC)
        {

        }

        public GenerateClientCodeAttribute(params Target[] targets)
            : this(1.0, targets)
        {
        }

        public GenerateClientCodeAttribute(double version, params Target[] targets)
        {
            this.Version = version;
            this.Targets = targets;
        }
    }
}
