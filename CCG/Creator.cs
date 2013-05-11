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
        private Assembly Ass { get; set; }
        private int Uid { get; set; }

        /// <summary>
        /// Creates client models of attributed classes in calling assembly 
        /// </summary>
        public void Create()
        {
            Ass = Assembly.GetCallingAssembly();
            Create(null, true);
        }

        /// <summary>
        /// Creates client models of classes within the specified namespace in calling assembly 
        /// </summary>
        /// <param name="namespace">namespace of classes to be translated to client</param>
        public void Create(string @namespace)
        {
            Ass = Assembly.GetCallingAssembly();
            Create(@namespace, false);
        }

        /// <summary>
        /// Creates client models of attributed classes within the specified namespace in calling assembly 
        /// </summary>
        /// <param name="namespace">namespace of classes to be translated to client</param>
        /// <param name="attributedOnly">include only GenerateClientModel attributed classes</param>
        public void Create(string @namespace, bool attributedOnly)
        {
            Uid++;

            if (Ass == null)
            {
                Ass = Assembly.GetCallingAssembly();
            }

            var types = Ass.GetTypes().Where(t => t.IsClass);

            if (!string.IsNullOrEmpty(@namespace))
            {
                types = Ass.GetTypes().Where(t => t.Namespace == @namespace);
            }

            if (attributedOnly)
            {
                types = types.Where(t => Attribute.IsDefined(t, typeof(GenerateClientModelAttribute)));
            }

            foreach (var t in types)
            {
                Generate(t);
            }
        }

        private void Generate(Type type)
        {
            bool hasAttribute = Attribute.IsDefined(type, typeof(GenerateClientModelAttribute));
            if (hasAttribute)
            {
                GenerateClientModelAttribute attribute = Attribute.GetCustomAttribute(type, typeof(GenerateClientModelAttribute)) as GenerateClientModelAttribute;
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
            else
            {
                GenerateClassForJava(type);
                GenerateClassForObjectiveC(type);
            }
        }

        private void GenerateClassForObjectiveC(Type type)
        {
            ObjectiveCTranslator translator = new ObjectiveCTranslator();
            var content = translator.Translate(type);
            Writer.ToFile(content, type.Name, "m", Uid);
        }

        private void GenerateClassForJava(Type type)
        {
            JavaTranslator translator = new JavaTranslator();
            var content = translator.Translate(type);
            Writer.ToFile(content, type.Name, "java", Uid);
        }
    }
}
