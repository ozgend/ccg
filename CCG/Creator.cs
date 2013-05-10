//  client code generator
//  ozgend

using denolk.CCG.Attributes;
using denolk.CCG.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace denolk.CCG
{
    public class Creator
    {
        public void Create(Type type)
        {
            Assembly ass = Assembly.GetAssembly(type);
            var types = ass.GetTypes().Where(t => Attribute.IsDefined(t, typeof(GenerateClientModelAttribute)));

            foreach (var t in types)
            {
                GenerateClientModelAttribute attribute = Attribute.GetCustomAttribute(t, typeof(GenerateClientModelAttribute)) as GenerateClientModelAttribute;
                Generate(t, attribute);
            }
        }

        private void Generate(Type type, GenerateClientModelAttribute attribute)
        {
            foreach (var target in attribute.Targets)
            {
                switch (target)
                {
                    case GenerateClientModelAttribute.Target.Java:
                        GenerateClassForJava(type);
                        break;

                    case GenerateClientModelAttribute.Target.ObjectiveC:
                        GenerateClassForObjectiveC(type);
                        break;
                }
            }
        }

        private void GenerateClassForObjectiveC(Type type)
        {
            ObjectiveCTranslator translator = new ObjectiveCTranslator();
            var content = translator.Translate(type);
            Writer.ToFile(content, type.Name, "m");
        }

        private void GenerateClassForJava(Type type)
        {
            JavaTranslator translator = new JavaTranslator();
            var content = translator.Translate(type);
            Writer.ToFile(content, type.Name, "java");
        }
    }
}
