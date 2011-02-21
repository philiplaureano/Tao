using System;
using System.Collections.Generic;
using System.Text;

namespace CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SampleAttribute : Attribute
    {
        public object obj; // field called "obj"
        public object SomeProperty { get; set; }
        //public SampleAttribute() { } // default ctor
        public SampleAttribute(object x) { }
    }    
}
