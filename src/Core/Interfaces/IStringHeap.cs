using System.Collections.Generic;

namespace Tao.Core
{
    /// <summary>
    /// Represents the metadata string heap.
    /// </summary>
    public interface IStringHeap : IReader
    {
        /// <summary>
        /// Gets the value indicating the list of strings that currently reside within the string heap.
        /// </summary>
        /// <value>The strings that are currently stored inside the string heap.</value>
        IList<string> Strings { get; }

        /// <summary>
        /// Obtains the string located at the given offset into the string heap.
        /// </summary>
        /// <param name="offset">The target string heap offset.</param>
        /// <returns>A string located at the given offset.</returns>
        string GetStringFromOffset(uint offset);
    }
}