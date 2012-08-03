using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleCustomAttributeUsage
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomAttributeWithUInt16ArgumentsAttribute : Attribute
    {
        private UInt16 _value;

        public CustomAttributeWithUInt16ArgumentsAttribute(UInt16 value)
        {
            _value = value;
        }

        public UInt16 Value
        {
            get { return _value; }
        }
    }
}
