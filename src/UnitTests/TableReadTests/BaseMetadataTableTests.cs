using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Tao.Containers;
using Tao.Interfaces;

namespace Tao.UnitTests.TableReadTests
{
    public class BaseMetadataTableTests : BaseStreamTests
    {
        protected void TestTableRead(Func<TableId, ITuple<int, Stream>> getTable, TableId tableId, int expectedRowCount, int expectedStreamLength)
        {
            var result = getTable(tableId);
            var tableStream = result.Item2;

            Assert.AreEqual(expectedRowCount, result.Item1, "Wrong row count");
            Assert.AreEqual(expectedStreamLength, tableStream.Length, "Wrong Stream Length");
        }

        protected void TestTableRead(TableId tableId, int expectedRowCount, int expectedStreamLength)
        {
            TestTableRead(GetTable, tableId, expectedRowCount, expectedStreamLength);
        }

        protected ITuple<int, Stream> GetTable(TableId tableId)
        {
            return GetTable(tableId, GetStream());
        }

        protected ITuple<int, Stream> GetTable(TableId tableId, Stream stream)
        {
            var reader = container.GetInstance<IFunction<Stream, IDictionary<TableId, ITuple<int, Stream>>>>("ReadAllMetadataTables");
            Assert.IsNotNull(reader);

            var tables = reader.Execute(stream);
            Assert.IsNotNull(tables);
            Assert.IsTrue(tables.ContainsKey(tableId));

            return tables[tableId];
        }
    }
}