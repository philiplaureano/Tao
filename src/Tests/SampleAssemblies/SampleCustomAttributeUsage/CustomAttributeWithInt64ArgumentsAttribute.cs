using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleCustomAttributeUsage
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomAttributeWithInt64ArgumentsAttribute : Attribute
    {
        private Int64 _value;

        public CustomAttributeWithInt64ArgumentsAttribute(Int64 value)
        {
            _value = value;
        }

        public Int64 Value
        {
            get { return _value; }
        }
    }
}
