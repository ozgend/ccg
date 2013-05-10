﻿using denolk.CCG.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace denolk.CCG
{
    internal class Writer
    {
        public static void ToFile(string content, string name, string extension)
        {
            //todo return archive file as memory stream maybe?

            var folder = string.Format(@"{0}\generated", Directory.GetCurrentDirectory());
            Directory.CreateDirectory(folder);
            var filepath = string.Format(@"{0}\{1}.{2}", folder, name, extension);
            File.WriteAllText(filepath, content);
        }
    }
}
