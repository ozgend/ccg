using denolk.CCG.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace denolk.CCG
{
    internal class Writer
    {
        public static void ToFile(string directory, string name, string content)
        {
            var folder = string.Format(@"{0}\generated", directory);
            Directory.CreateDirectory(folder);
            var filepath = string.Format(@"{0}\{1}", folder, name);
            File.WriteAllText(filepath, content);
        }
    }
}
