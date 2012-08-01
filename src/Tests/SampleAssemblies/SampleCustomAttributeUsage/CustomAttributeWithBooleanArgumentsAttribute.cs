using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleCustomAttributeUsage
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomAttributeWithBooleanArgumentsAttribute : Attribute
    {
        private bool _value;

        public CustomAttributeWithBooleanArgumentsAttribute(bool value)
        {
            _value = value;
        }

        public bool Value
        {
            get { return _value; }
        }
    }
}
