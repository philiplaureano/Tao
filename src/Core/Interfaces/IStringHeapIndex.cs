namespace Tao.Core
{
    /// <summary>
    /// Represents an index that points to a string within the string heap.
    /// </summary>
    public interface IStringHeapIndex : IHeapIndex
    {
        /// <summary>
        /// Reads the string associated with the current index <see cref="HeapIndex.Value"/>.
        /// </summary>
        /// <param name="stringHeap">The string heap that contains the target string.</param>
        /// <returns>The string associated with the given heap index.</returns>
        string GetText(IStringHeap stringHeap);
    }
}