using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace denolk.CCG.Translator
{
    abstract class TranslatorBase
    {
        public abstract string Translate(Type type);
        protected abstract string CreateProperty(PropertyInfo info);
        protected abstract string GetDestinationType(PropertyInfo info);
    }
}
