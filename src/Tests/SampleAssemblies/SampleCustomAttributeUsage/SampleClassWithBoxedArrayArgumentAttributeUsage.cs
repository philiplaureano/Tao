using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleCustomAttributeUsage
{
    [CustomAttributeWithObjectArguments(new []{1, 2, 3, 4, 5, 6})]
    public class SampleClassWithBoxedArrayArgumentAttributeUsage
    {
    }
}
