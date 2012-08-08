using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleCustomAttributeUsage
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomAttributeWithBoxedEnumArgumentsAttribute : Attribute
    {
        private object _value;

        public CustomAttributeWithBoxedEnumArgumentsAttribute(object value)
        {
            _value = value;
        }

        public object Value
        {
            get { return _value; }
        }
    }
}
