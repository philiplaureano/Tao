using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleCustomAttributeUsage
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomAttributeWithSampleEnumArgumentsAttribute : Attribute
    {
        private SampleEnum _value;

        public CustomAttributeWithSampleEnumArgumentsAttribute(SampleEnum value)
        {
            _value = value;
        }

        public SampleEnum Value
        {
            get { return _value; }
        }
    }
}
