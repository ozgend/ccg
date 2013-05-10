using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace denolk.CCG.Translator
{
    internal class ObjectiveCTranslator : TranslatorBase
    {
        public override string Translate(Type type)
        {
            var properties = type.GetProperties();

            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            sb1.AppendFormat("#import <Foundation/Foundation.h> \n\n @interface {0} : NSObject \n", type.Name);
            sb2.AppendFormat("\n\n #import <{0}.h> \n\n @implementation {0}\n", type.Name);
            sb2.AppendFormat("@synthesize {0};\n", string.Join(",", properties.Select(p => p.Name)));

            foreach (var info in properties)
            {
                var prop = CreateProperty(info);
                sb1.Append(prop);
            }

            sb1.AppendFormat("@end");
            sb2.AppendFormat("@end");

            return sb1.Append(sb2.ToString()).ToString();
        }

        protected override string CreateProperty(PropertyInfo info)
        {
            return string.Format("@property{0} {1};\n", GetDestinationType(info), info.Name);
        }

        protected override string GetDestinationType(PropertyInfo info)
        {
            var type = info.PropertyType;

            if (typeof(System.Byte) == type)
            {
                return "(nonatomic,readwrite) byte";
            }
            else if (typeof(System.Char) == type)
            {
                return "(nonatomic,readwrite) char";
            }
            else if (typeof(System.Decimal) == type)
            {
                return "(nonatomic,retain) NSNumber *";
            }
            else if (typeof(System.Double) == type)
            {
                return "(nonatomic,retain) NSNumber *";
            }
            else if (typeof(System.Int32) == type)
            {
                return "(nonatomic,retain) NSNumber *";
            }
            else if (typeof(System.Int64) == type)
            {
                return "(nonatomic,retain) NSNumber *";
            }
            else if (typeof(System.Boolean) == type)
            {
                return "(nonatomic,readwrite) BOOL";
            }
            else if (typeof(System.String) == type)
            {
                return "(nonatomic,retain) NSString *";
            }
            else if (typeof(System.Object) == type)
            {
                return "(nonatomic,retain) NSObject *";
            }
            else if (typeof(IEnumerable<System.Object>) == type)
            {
                return "(nonatomic,retain) NSMutableArray *";
            }
            else
            {
                return type.Name;
            }
        }

    }
}
