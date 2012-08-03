using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleCustomAttributeUsage
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomAttributeWithSignedByteArgumentsAttribute : Attribute
    {
        private sbyte _value;

        public CustomAttributeWithSignedByteArgumentsAttribute(sbyte value)
        {
            _value = value;
        }

        public sbyte Value
        {
            get { return _value; }
        }
    }
}
