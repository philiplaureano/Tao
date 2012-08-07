using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleCustomAttributeUsage
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomAttributeWithArrayArgumentsAttribute : Attribute
    {
        private object[] _value;

        public CustomAttributeWithArrayArgumentsAttribute(object[] value)
        {
            _value = value;
        }

        public object[] Value
        {
            get { return _value; }
        }
    }
}
