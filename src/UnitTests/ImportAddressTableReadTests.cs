using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Tao.UnitTests
{
    [TestFixture]
    public class ImportAddressTableReadTests : BaseHeaderReadTest
    {
        [Test]
        public void ShouldBeAbleToReadIAT()
        {
            // Notes: 
            // File Offset = RVA - (Section Alignment * section number)
            throw new NotImplementedException();
        }
    }
}
