using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleCustomAttributeUsage
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomAttributeWithUInt64ArgumentsAttribute : Attribute
    {
        private ulong _value;

        public CustomAttributeWithUInt64ArgumentsAttribute(ulong value)
        {
            _value = value;
        }

        public ulong Value
        {
            get { return _value; }
        }
    }
}
