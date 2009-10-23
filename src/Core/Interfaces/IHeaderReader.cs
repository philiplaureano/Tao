using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents a type that can read multiple items from a given binary reader.
    /// </summary>
    /// <typeparam name="TItem">The item type.</typeparam>
    public interface IHeaderReader<TItem>
    {
        /// <summary>
        /// Reads a set of items from the given binary reader.
        /// </summary>
        /// <param name="itemCount">The number of items to read.</param>
        /// <param name="reader">The binary reader.</param>
        /// <returns>The list of items read from the reader.</returns>
        IEnumerable<TItem> ReadFrom(int itemCount, IBinaryReader reader);
    }
}
