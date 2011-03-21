using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tao.Interfaces;
namespace Tao.UnitTests
{
    public static class MetadataTableTestExtensions
    {
        public static void ShouldHaveExpectedRowCount(this IDictionary<TableId, ITuple<int, Stream>> tables, TableId tableId, int expectedRowCount)
        {            
            Assert.IsTrue(tables.ContainsKey(tableId), "Missing metadata table '{0}'", tableId);
            Assert.AreEqual(expectedRowCount, tables[tableId].Item1, "Invalid row count for table '{0}'", tableId);
        }
    }
}
