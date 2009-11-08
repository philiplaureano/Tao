using System.Collections.Generic;

namespace Tao.Core
{
    /// <summary>
    /// Represents the metadata string heap.
    /// </summary>
    public interface IStringHeap
    {
        /// <summary>
        /// Gets the value indicating the list of strings that currently reside within the string heap.
        /// </summary>
        /// <value>The strings that are currently stored inside the string heap.</value>
        IList<string> Strings { get; }
    }
}