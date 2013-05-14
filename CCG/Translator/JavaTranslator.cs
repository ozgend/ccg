using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace denolk.CCG.Translator
{
    internal class JavaTranslator : ITranslator
    {
        public TranslatedType Translate(Type @class)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("public class {0} {{\n", @class.Name);

            foreach (var info in @class.GetProperties())
            {
                var prop = CreateProperty(info);
                sb.Append(prop);
            }

            sb.AppendFormat("}}");

            var content = sb.ToString();
            return new TranslatedType
            {
                Name = string.Format("{0}.java", @class.Name),
                Content = content
            };
        }

        public string CreateProperty(PropertyInfo property)
        {
            return string.Format("\tpublic {0} {1};\n", GetDestinationType(property), property.Name);
        }

        public string GetDestinationType(PropertyInfo property)
        {
            var type = property.PropertyType;

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
