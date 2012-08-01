using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleCustomAttributeUsage
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomAttributeWithByteArgumentsAttribute : Attribute
    {
        private byte _value;

        public CustomAttributeWithByteArgumentsAttribute(byte value)
        {
            _value = value;
        }

        public byte Value
        {
            get { return _value; }
        }
    }
}
