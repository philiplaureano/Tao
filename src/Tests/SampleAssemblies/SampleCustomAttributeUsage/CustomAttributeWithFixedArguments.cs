using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleCustomAttributeUsage
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomAttributeWithFixedArgumentsAttribute : Attribute
    {
        private int _value;

        public CustomAttributeWithFixedArgumentsAttribute(int value)
        {
            _value = value;
        }

        public int Value
        {
            get { return _value; }
        }
    }
}
