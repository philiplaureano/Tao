using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleCustomAttributeUsage
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomAttributeWithTypeArgumentsAttribute : Attribute
    {
        private Type _value;

        public CustomAttributeWithTypeArgumentsAttribute(Type value)
        {
            _value = value;
        }

        public Type Value
        {
            get { return _value; }
        }
    }
}
