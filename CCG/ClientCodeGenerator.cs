using denolk.CCG.Attributes;
using denolk.CCG.Translator;
using denolk.CCG.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace denolk.CCG
{
    public class ClientCodeGenerator
    {
        private Assembly Ass { get; set; }

        /// <summary>
        /// Creates client models of attributed classes in calling assembly 
        /// </summary>
        /// <param name="directory">ouput directory where generated files will be created</param>
        public void ToFiles(string directory)
        {
            Ass = Assembly.GetCallingAssembly();
            Generate(null, true).WriteToFile(directory);
        }

        /// <summary>
        /// Creates client models of classes within the specified namespace in calling assembly 
        /// </summary>
        /// <param name="namespace">namespace of classes to be translated to client</param>
        /// <param name="directory">ouput directory where generated files will be created</param>
        public void ToFiles(string directory, string @namespace)
        {
            Ass = Assembly.GetCallingAssembly();
            Generate(@namespace, false).WriteToFile(directory);
        }

        /// <summary>
        /// Creates client models of attributed classes within the specified namespace in calling assembly 
        /// </summary>
        /// <param name="namespace">namespace of classes to be translated to client</param>
        /// <param name="attributedOnly">include only GenerateClientModel attributed classes</param>
        /// <param name="directory">ouput directory where generated files will be created</param>
        public void ToFiles(string directory, string @namespace, bool attributedOnly)
        {
            Ass = Assembly.GetCallingAssembly();
            Generate(@namespace, attributedOnly).WriteToFile(directory);
        }

        /// <summary>
        /// Creates client models of attributed classes in calling assembly 
        /// </summary>
        /// <returns>filename and filecontent pairs</returns>
        public Dictionary<string, string> ToDictionary()
        {
            Ass = Assembly.GetCallingAssembly();
            return Generate(null, true);
        }

        /// <summary>
        /// Creates client models of classes within the specified namespace in calling assembly 
        /// </summary>
        /// <param name="namespace">namespace of classes to be translated to client</param>
        /// <returns>filename and filecontent pairs</returns>
        public Dictionary<string, string> ToDictionary(string @namespace)
        {
            Ass = Assembly.GetCallingAssembly();
            return Generate(@namespace, false);
        }

        /// <summary>
        /// Creates client models of attributed classes within the specified namespace in calling assembly 
        /// </summary>
        /// <param name="namespace">namespace of classes to be translated to client</param>
        /// <param name="attributedOnly">include only GenerateClientModel attributed classes</param>
        /// <returns>filename and filecontent pairs</returns>
        public Dictionary<string, string> ToDictionary(string @namespace, bool attributedOnly)
        {
            Ass = Assembly.GetCallingAssembly();
            return Generate(@namespace, attributedOnly);
        }

        private Dictionary<string, string> Generate(string @namespace, bool attributedOnly)
        {
            Dictionary<string, string> generated = new Dictionary<string, string>();

            var types = Ass.GetTypes().Where(t => t.IsClass);

            if (!string.IsNullOrEmpty(@namespace))
            {
                types = Ass.GetTypes().Where(t => t.Namespace.StartsWith(@namespace));
            }

            if (attributedOnly)
            {
                types = types.Where(t => Attribute.IsDefined(t, typeof(GenerateClientCodeAttribute)));
            }

            foreach (var @class in types)
            {
                var list = @class.TranslatedTypes();
                foreach (var trans in list)
                {
                    generated.Add(trans.Name, trans.Content);
                }
            }

            return generated;
        }


    }
}
