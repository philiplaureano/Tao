using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleCustomAttributeUsage
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomAttributeWithCharArgumentsAttribute : Attribute
    {
        private char _value;

        public CustomAttributeWithCharArgumentsAttribute(char value)
        {
            _value = value;
        }

        public char Value
        {
            get { return _value; }
        }
    }
}
