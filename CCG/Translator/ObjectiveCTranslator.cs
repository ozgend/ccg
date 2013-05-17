using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace denolk.CCG.Translator
{
    internal class ObjectiveCTranslator : ITranslator
    {
        public TranslatedType Translate(Type @class)
        {
            var properties = @class.GetProperties();

            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            sb1.AppendFormat("#import <Foundation/Foundation.h>\n\n@interface {0} : NSObject\n", @class.Name);
            sb2.AppendFormat("#import <{0}.h>\n\n@implementation {0}\n", @class.Name);
            sb2.AppendFormat("@synthesize {0};\n", string.Join(",", properties.Select(p => p.Name)));

            foreach (var property in properties)
            {
                var prop = CreateProperty(property);
                sb1.Append(prop);
            }

            sb1.AppendFormat("@end\n\n");
            sb2.AppendFormat("@end");

            var content = sb1.Append(sb2.ToString()).ToString();

            return new TranslatedType
            {
                Name = string.Format("{0}.m", @class.Name),
                Content = content
            };
        }

        public string CreateProperty(PropertyInfo property)
        {
            return string.Format("@property{0} {1};\n", GetDestinationType(property), property.Name);
        }

        public string GetDestinationType(PropertyInfo property)
        {
            var type = property.PropertyType;

            if (typeof(System.Byte) == type)
            {
                return "(nonatomic,readwrite) byte";
            }
            else if (typeof(System.Char) == type)
            {
                return "(nonatomic,readwrite) char";
            }
            else if (typeof(System.DateTime) == type)
            {
                return "(nonatomic,retain) NSDate *";
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
            else if (typeof(IEnumerable<System.Object>) == type || typeof(List<System.Object>) == type || type.Name.Equals("List`1") || type.Name.Equals("IEnumerable`1"))
            {
                return "(nonatomic,retain) NSMutableArray *";
            }
            else
            {
                return string.Format("(nonatomic,retain) {0} *", type.Name);
            }
        }

    }
}
