using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace denolk.CCG.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GenerateClientModelAttribute : Attribute
    {
        public enum Target
        {
            Java, ObjectiveC
        }

        public double Version { get; set; }
        public Target[] Targets { get; set; }

        public GenerateClientModelAttribute()
            : this(1.0, Target.Java, Target.ObjectiveC)
        {

        }

        public GenerateClientModelAttribute(params Target[] targets)
            : this(1.0, targets)
        {
        }

        public GenerateClientModelAttribute(double version, params Target[] targets)
        {
            this.Version = version;
            this.Targets = targets;
        }
    }
}
