using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace denolk.CCG.Translator
{
    internal class JavaTranslator : TranslatorBase
    {
        public override string Translate(Type type)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("public class {0} {{\n", type.Name);

            foreach (var info in type.GetProperties())
            {
                var prop = CreateProperty(info);
                sb.Append(prop);
            }

            sb.AppendFormat("}}");

            return sb.ToString();
        }

        protected override string CreateProperty(PropertyInfo info)
        {
            return string.Format("public {0} {1};\n", GetDestinationType(info), info.Name);
        }

        protected override string GetDestinationType(PropertyInfo info)
        {
            var type = info.PropertyType;

            if (typeof(System.Byte) == type)
            {
                return "byte";
            }
            else if (typeof(System.Char) == type)
            {
                return "char";
            }
            else if (typeof(System.Decimal) == type)
            {
                return "double";
            }
            else if (typeof(System.Double) == type)
            {
                return "double";
            }
            else if (typeof(System.Int32) == type)
            {
                return "int";
            }
            else if (typeof(System.Int64) == type)
            {
                return "long";
            }
            else if (typeof(System.Boolean) == type)
            {
                return "boolean";
            }
            else if (typeof(System.String) == type)
            {
                return "String";
            }
            else if (typeof(System.Object) == type)
            {
                return "Object";
            }
            else if (typeof(IEnumerable<System.Object>) == type)
            {
                return "ArrayList<Object>";
            }
            else
            {
                return type.Name;
            }
        }

    }
}
