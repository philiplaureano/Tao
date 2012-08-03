using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleCustomAttributeUsage
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomAttributeWithFloatArgumentsAttribute : Attribute
    {
        private float _value;

        public CustomAttributeWithFloatArgumentsAttribute(float value)
        {
            _value = value;
        }

        public float Value
        {
            get { return _value; }
        }
    }
}
