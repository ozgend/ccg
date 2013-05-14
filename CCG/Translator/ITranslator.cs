using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace denolk.CCG.Translator
{
    internal interface ITranslator
    {
        TranslatedType Translate(Type @class);
        string CreateProperty(PropertyInfo property);
        string GetDestinationType(PropertyInfo property);
    }
}
