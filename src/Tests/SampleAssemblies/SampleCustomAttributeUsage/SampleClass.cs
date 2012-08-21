using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleCustomAttributeUsage
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FooAttribute : Attribute
    {
    }

    [CustomAttributeWithFixedArguments(42)]
    [Foo]
    public class SampleClass
    {
    }
}
