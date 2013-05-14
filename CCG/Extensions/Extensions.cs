using denolk.CCG.Attributes;
using denolk.CCG.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace denolk.CCG.Extensions
{
    internal static class Extensions
    {
        public static List<TranslatedType> TranslatedTypes(this Type @class)
        {
            List<TranslatedType> list = new List<TranslatedType>();
            JavaTranslator javaTranslator = new JavaTranslator();
            ObjectiveCTranslator objcTranslator = new ObjectiveCTranslator();

            bool hasAttribute = Attribute.IsDefined(@class, typeof(GenerateClientCodeAttribute));
            if (hasAttribute)
            {
                GenerateClientCodeAttribute attribute = Attribute.GetCustomAttribute(@class, typeof(GenerateClientCodeAttribute)) as GenerateClientCodeAttribute;
                foreach (var target in attribute.Targets)
                {
                    switch (target)
                    {
                        case GenerateClientCodeAttribute.Target.Java:
                            list.Add(javaTranslator.Translate(@class));
                            break;

                        case GenerateClientCodeAttribute.Target.ObjectiveC:
                            list.Add(objcTranslator.Translate(@class));
                            break;
                    }
                }
            }
            else
            {
                list.Add(javaTranslator.Translate(@class));
                list.Add(objcTranslator.Translate(@class));
            }

            return list;
        }

        public static TranslatedType TranslateTo<T>(this Type @class) where T : ITranslator
        {
            ITranslator translator = Activator.CreateInstance(typeof(T)) as ITranslator;
            var content = translator.Translate(@class);
            return content;
        }

        public static void WriteToFile(this Dictionary<string, string> dictionary, string directory)
        {
            foreach (var pair in dictionary)
            {
                Writer.ToFile(pair.Key, pair.Value, directory);
            }
        }

    }
}
