using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Interfaces
{
    /// <summary>
    /// Represents a data structure that has a specific number and sequence of values.
    /// </summary>
    public interface ITuple<T>
    {
        /// <summary>
        /// Gets the value indicating the value of Item1.
        /// </summary>
        /// <value>The value of Item1.</value>
        T Item1 { get; }
    }

    /// <summary>
    /// Represents a data structure that has a specific number and sequence of values.
    /// </summary>
    public interface ITuple<T1, T2> : ITuple<T1>
    {
        /// <summary>
        /// Gets the value indicating the value of Item2.
        /// </summary>
        /// <value>The value of Item2.</value>
        T2 Item2 { get; }
    }

    /// <summary>
    /// Represents a data structure that has a specific number and sequence of values.
    /// </summary>
    public interface ITuple<T1, T2, T3> : ITuple<T1, T2>
    {
        /// <summary>
        /// Gets the value indicating the value of Item3.
        /// </summary>
        /// <value>The value of Item2.</value>
        T3 Item3 { get; }
    }

    /// <summary>
    /// Represents a data structure that has a specific number and sequence of values.
    /// </summary>
    public interface ITuple<T1, T2, T3, T4> : ITuple<T1, T2, T3>
    {
        /// <summary>
        /// Gets the value indicating the value of Item4.
        /// </summary>
        /// <value>The value of Item4.</value>
        T4 Item4 { get; }
    }    
}
