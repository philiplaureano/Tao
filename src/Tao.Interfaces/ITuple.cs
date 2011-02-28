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

    /// <summary>
    /// Represents a data structure that has a specific number and sequence of values.
    /// </summary>
    public interface ITuple<T1, T2, T3, T4, T5> : ITuple<T1, T2, T3, T4>
    {
        /// <summary>
        /// Gets the value indicating the value of Item5.
        /// </summary>
        /// <value>The value of Item5.</value>
        T5 Item5 { get; }
    }

    /// <summary>
    /// Represents a data structure that has a specific number and sequence of values.
    /// </summary>
    public interface ITuple<T1, T2, T3, T4, T5, T6> : ITuple<T1, T2, T3, T4, T5>
    {
        /// <summary>
        /// Gets the value indicating the value of Item6.
        /// </summary>
        /// <value>The value of Item6.</value>
        T6 Item6 { get; }
    }

    /// <summary>
    /// Represents a data structure that has a specific number and sequence of values.
    /// </summary>
    public interface ITuple<T1, T2, T3, T4, T5, T6, T7> : ITuple<T1, T2, T3, T4, T5, T6>
    {
        /// <summary>
        /// Gets the value indicating the value of Item7.
        /// </summary>
        /// <value>The value of Item7.</value>
        T7 Item7 { get; }
    }

    /// <summary>
    /// Represents a data structure that has a specific number and sequence of values.
    /// </summary>
    public interface ITuple<T1, T2, T3, T4, T5, T6, T7, T8> : ITuple<T1, T2, T3, T4, T5, T6, T7>
    {
        /// <summary>
        /// Gets the value indicating the value of Item8.
        /// </summary>
        /// <value>The value of Item8.</value>
        T8 Item8 { get; }
    }

    /// <summary>
    /// Represents a data structure that has a specific number and sequence of values.
    /// </summary>
    public interface ITuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> : ITuple<T1, T2, T3, T4, T5, T6, T7, T8>
    {
        /// <summary>
        /// Gets the value indicating the value of Item9.
        /// </summary>
        /// <value>The value of Item9.</value>
        T9 Item9 { get; }
    }
}
