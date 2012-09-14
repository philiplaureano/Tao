using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleTypeDefRowUsage
{
    public class SampleClassWithMultipleInterfaces : IFoo, IBar
    {
        private int _field1;
        private string _field2;

        public void DoFoo()
        {
            throw new NotImplementedException();
        }

        public void DoBar()
        {
            throw new NotImplementedException();
        }
    }
}
