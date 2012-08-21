using System;

namespace SampleCustomAttributeUsage
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FooAttribute : Attribute
    {
    }
}