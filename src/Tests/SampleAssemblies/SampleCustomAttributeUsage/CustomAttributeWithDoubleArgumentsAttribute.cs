using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleCustomAttributeUsage
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomAttributeWithDoubleArgumentsAttribute : Attribute
    {
        private double _value;

        public CustomAttributeWithDoubleArgumentsAttribute(double value)
        {
            _value = value;
        }

        public double Value
        {
            get { return _value; }
        }
    }
}
