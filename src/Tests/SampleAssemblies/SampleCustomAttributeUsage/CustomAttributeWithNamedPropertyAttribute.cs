using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleCustomAttributeUsage
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomAttributeWithNamedPropertyAttribute : Attribute
    {
        public int SomeProperty { get; set; }
    }
}
