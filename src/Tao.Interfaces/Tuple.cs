using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Interfaces
{
    /// <summary>
    /// Represents a data structure that has a specific number and sequence of values.
    /// </summary>
    public class Tuple<T> : ITuple<T>
    {
        private readonly T _item1;

        /// <summary>
        /// Initializes a new instance of the Tuple class.
        /// </summary>
        /// <param name="item1">The first item.</param>
        public Tuple(T item1)
        {
            _item1 = item1;
        }

        /// <summary>
        /// Gets the value indicating the value of Item1.
        /// </summary>
        /// <value>The value of Item1.</value>
        public T Item1
        {
            get
            {
                return _item1;
            }
        }
    }

    /// <summary>
    /// Represents a data structure that has a specific number and sequence of values.
    /// </summary>
    public class Tuple<T1, T2> : Tuple<T1>, ITuple<T1, T2>
    {
        private readonly T2 _item2;
        /// <summary>
        /// Initializes a new instance of the Tuple class.
        /// </summary>
        /// <param name="item1">The first item.</param>
        /// <param name="item2">The second item.</param>
        public Tuple(T1 item1, T2 item2) : base(item1)
        {
            _item2 = item2;
        }

        /// <summary>
        /// Gets the value indicating the value of Item2.
        /// </summary>
        /// <value>The value of Item2.</value>
        public T2 Item2
        {
            get
            {
                return _item2;
            }
        }
    }

    /// <summary>
    /// Represents a data structure that has a specific number and sequence of values.
    /// </summary>
    public class Tuple<T1, T2, T3> : Tuple<T1, T2>, ITuple<T1, T2, T3>
    {
        private readonly T3 _item3;

        /// <summary>
        /// Initializes a new instance of the Tuple class.
        /// </summary>
        /// <param name="item1">The first item.</param>
        /// <param name="item2">The second item.</param>
        /// <param name="item3">The third item.</param>
        public Tuple(T1 item1, T2 item2, T3 item3)
            : base(item1, item2)
        {
            _item3 = item3;
        }

        /// <summary>
        /// Gets the value indicating the value of Item3.
        /// </summary>
        /// <value>The value of Item3.</value>
        public T3 Item3
        {
            get
            {
                return _item3;
            }
        }
    }

    /// <summary>
    /// Represents a data structure that has a specific number and sequence of values.
    /// </summary>
    public class Tuple<T1, T2, T3, T4> : Tuple<T1, T2, T3>, ITuple<T1, T2, T3, T4>
    {
        private readonly T4 _item4;

        /// <summary>
        /// Initializes a new instance of the Tuple class.
        /// </summary>
        /// <param name="item1">The first item.</param>
        /// <param name="item2">The second item.</param>
        /// <param name="item3">The third item.</param>
        /// <param name="item4">The fourth item.</param>
        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4)
            : base(item1, item2, item3)
        {
            _item4 = item4;
        }

        /// <summary>
        /// Gets the value indicating the value of Item4.
        /// </summary>
        /// <value>The value of Item4.</value>
        public T4 Item4
        {
            get
            {
                return _item4;
            }
        }
    }

    /// <summary>
    /// Represents an extension class that allows users to instantiate tuple objects.
    /// </summary>
    public static class Tuple
    {        
        /// <summary>
        /// Creates an instance of a tuple class.
        /// </summary>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="item">The value of item1.</param>
        /// <returns>A tuple that contains the given items.</returns>
        public static ITuple<T> New<T>(T item)
        {
            return new Tuple<T>(item);
        }

        /// <summary>
        /// Creates an instance of a tuple class.
        /// </summary>
        /// <typeparam name="T1">The item1 type.</typeparam>
        /// <typeparam name="T2">The item2 type.</typeparam>>
        /// <param name="item1">The value of item1.</param>
        /// <param name="item2">The value of item2.</param>
        /// <returns>A tuple that contains the given items.</returns>
        public static ITuple<T1, T2> New<T1, T2>(T1 item1, T2 item2)
        {
            return new Tuple<T1, T2>(item1, item2);
        }

        /// <summary>
        /// Creates an instance of a tuple class.
        /// </summary>
        /// <typeparam name="T1">The item1 type.</typeparam>
        /// <typeparam name="T2">The item2 type.</typeparam>
        /// <typeparam name="T3">The item3 type.</typeparam>
        /// <param name="item1">The value of item1.</param>
        /// <param name="item2">The value of item2.</param>
        /// <param name="item3">The value of item3.</param>
        /// <returns>A tuple that contains the given items.</returns>
        public static ITuple<T1, T2, T3> New<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
        {
            return new Tuple<T1, T2, T3>(item1, item2, item3);
        }

        /// <summary>
        /// Creates an instance of a tuple class.
        /// </summary>
        /// <typeparam name="T1">The item1 type.</typeparam>
        /// <typeparam name="T2">The item2 type.</typeparam>
        /// <typeparam name="T3">The item3 type.</typeparam>
        /// <typeparam name="T4">The item4 type.</typeparam>
        /// <param name="item1">The value of item1.</param>
        /// <param name="item2">The value of item2.</param>
        /// <param name="item3">The value of item3.</param>
        /// <param name="item4">The value of item4.</param>
        /// <returns>A tuple that contains the given items.</returns>
        public static ITuple<T1, T2, T3, T4> New<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            return new Tuple<T1, T2, T3, T4>(item1, item2, item3, item4);
        }        

        /// <summary>
        /// Executes the given function using a tuple from the given arguments.
        /// </summary>
        /// <typeparam name="T1">The item1 type.</typeparam>
        /// <typeparam name="T2">The item2 type.</typeparam>
        /// <typeparam name="TOutput">The return type.</typeparam>
        /// <param name="function">The target function.</param>
        /// <param name="item1">The value of item1.</param>
        /// <param name="item2">The value of item2.</param>
        /// <returns>The value returned by the function.</returns>
        public static TOutput Execute<T1, T2, TOutput>(this IFunction<ITuple<T1, T2>, TOutput> function, T1 item1, T2 item2)
        {
            return function.Execute(Tuple.New(item1, item2));
        }

        /// <summary>
        /// Executes the given function using a tuple from the given arguments.
        /// </summary>
        /// <typeparam name="T1">The item1 type.</typeparam>
        /// <typeparam name="T2">The item2 type.</typeparam>
        /// <typeparam name="T3">The item3 type.</typeparam>
        /// <typeparam name="TOutput">The return type.</typeparam>
        /// <param name="function">The target function.</param>
        /// <param name="item1">The value of item1.</param>
        /// <param name="item2">The value of item2.</param>
        /// <param name="item3">The value of item3.</param>
        /// <returns>The value returned by the function.</returns>
        public static TOutput Execute<T1, T2, T3, TOutput>(this IFunction<ITuple<T1, T2, T3>, TOutput> function, T1 item1, T2 item2, T3 item3)
        {
            return function.Execute(Tuple.New(item1, item2, item3));
        }

        /// <summary>
        /// Executes the given function using a tuple from the given arguments.
        /// </summary>
        /// <typeparam name="T1">The item1 type.</typeparam>
        /// <typeparam name="T2">The item2 type.</typeparam>
        /// <typeparam name="T3">The item3 type.</typeparam>
        /// <typeparam name="T4">The item4 type.</typeparam>
        /// <typeparam name="TOutput">The return type.</typeparam>
        /// <param name="function">The target function.</param>
        /// <param name="item1">The value of item1.</param>
        /// <param name="item2">The value of item2.</param>
        /// <param name="item3">The value of item3.</param>
        /// <param name="item4">The value of item3.</param>
        /// <returns>The value returned by the function.</returns>
        public static TOutput Execute<T1, T2, T3, T4, TOutput>(this IFunction<ITuple<T1, T2, T3, T4>, TOutput> function, T1 item1, T2 item2, T3 item3, T4 item4)
        {
            return function.Execute(Tuple.New(item1, item2, item3, item4));
        }

        /// <summary>
        /// Executes the given function using a tuple from the given arguments.
        /// </summary>
        /// <typeparam name="T1">The item1 type.</typeparam>
        /// <typeparam name="T2">The item2 type.</typeparam>
        /// <param name="function">The target function.</param>
        /// <param name="item1">The value of item1.</param>
        /// <param name="item2">The value of item2.</param>
        public static void Execute<T1, T2>(this IFunction<ITuple<T1, T2>> function, T1 item1, T2 item2)
        {
            function.Execute(Tuple.New(item1, item2));
        }

        /// <summary>
        /// Executes the given function using a tuple from the given arguments.
        /// </summary>
        /// <typeparam name="T1">The item1 type.</typeparam>
        /// <typeparam name="T2">The item2 type.</typeparam>
        /// <typeparam name="T3">The item3 type.</typeparam>
        /// <param name="function">The target function.</param>
        /// <param name="item1">The value of item1.</param>
        /// <param name="item2">The value of item2.</param>
        /// <param name="item3">The value of item3.</param>
        public static void Execute<T1, T2, T3>(this IFunction<ITuple<T1, T2, T3>> function, T1 item1, T2 item2, T3 item3)
        {
            function.Execute(Tuple.New(item1, item2, item3));
        }

        /// <summary>
        /// Executes the given function using a tuple from the given arguments.
        /// </summary>
        /// <typeparam name="T1">The item1 type.</typeparam>
        /// <typeparam name="T2">The item2 type.</typeparam>
        /// <typeparam name="T3">The item3 type.</typeparam>
        /// <typeparam name="T4">The item4 type.</typeparam>
        /// <param name="function">The target function.</param>
        /// <param name="item1">The value of item1.</param>
        /// <param name="item2">The value of item2.</param>
        /// <param name="item3">The value of item3.</param>
        /// <param name="item4">The value of item3.</param>
        public static void Execute<T1, T2, T3, T4>(this IFunction<ITuple<T1, T2, T3, T4>> function, T1 item1, T2 item2, T3 item3, T4 item4)
        {
            function.Execute(Tuple.New(item1, item2, item3, item4));
        }
    }
}