using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleCustomAttributeUsage
{
    [CustomAttributeWithFixedArguments(42)]
    [Foo]
    public class SampleClass
    {
    }
}
