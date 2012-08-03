using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleCustomAttributeUsage
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomAttributeWithInt16ArgumentsAttribute : Attribute
    {
        private Int16 _value;

        public CustomAttributeWithInt16ArgumentsAttribute(Int16 value)
        {
            _value = value;
        }

        public Int16 Value
        {
            get { return _value; }
        }
    }
}
