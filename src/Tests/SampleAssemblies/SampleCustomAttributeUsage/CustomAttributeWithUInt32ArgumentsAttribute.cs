using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleCustomAttributeUsage
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomAttributeWithUInt32ArgumentsAttribute : Attribute
    {
        private uint _value;

        public CustomAttributeWithUInt32ArgumentsAttribute(uint value)
        {
            _value = value;
        }

        public uint Value
        {
            get { return _value; }
        }
    }
}
