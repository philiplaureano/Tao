using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Tao.Interfaces
{
    /// <summary>
    /// Represents a data structure that has a specific number and sequence of values.
    /// </summary>
    [DebuggerStepThrough]
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
    [DebuggerStepThrough]
    public class Tuple<T1, T2> : Tuple<T1>, ITuple<T1, T2>
    {
        private readonly T2 _item2;
        /// <summary>
        /// Initializes a new instance of the Tuple class.
        /// </summary>
        /// <param name="item1">The first item.</param>
        /// <param name="item2">The second item.</param>
        public Tuple(T1 item1, T2 item2)
            : base(item1)
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
    [DebuggerStepThrough]
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
    [DebuggerStepThrough]
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
    /// Represents a data structure that has a specific number and sequence of values.
    /// </summary>
    [DebuggerStepThrough]
    public class Tuple<T1, T2, T3, T4, T5> : Tuple<T1, T2, T3, T4>, ITuple<T1, T2, T3, T4, T5>
    {
        private readonly T5 _item5;

        /// <summary>
        /// Initializes a new instance of the Tuple class.
        /// </summary>
        /// <param name="item1">The first item.</param>
        /// <param name="item2">The second item.</param>
        /// <param name="item3">The third item.</param>
        /// <param name="item4">The fourth item.</param>
        /// <param name="item5">The fifth item.</param>
        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
            : base(item1, item2, item3, item4)
        {
            _item5 = item5;
        }

        /// <summary>
        /// Gets the value indicating the value of Item4.
        /// </summary>
        /// <value>The value of Item4.</value>
        public T5 Item5
        {
            get
            {
                return _item5;
            }
        }
    }

    /// <summary>
    /// Represents a data structure that has a specific number and sequence of values.
    /// </summary>
    [DebuggerStepThrough]
    public class Tuple<T1, T2, T3, T4, T5, T6> : Tuple<T1, T2, T3, T4, T5>, ITuple<T1, T2, T3, T4, T5, T6>
    {
        private readonly T6 _item6;

        /// <summary>
        /// Initializes a new instance of the Tuple class.
        /// </summary>
        /// <param name="item1">The first item.</param>
        /// <param name="item2">The second item.</param>
        /// <param name="item3">The third item.</param>
        /// <param name="item4">The fourth item.</param>
        /// <param name="item5">The fifth item.</param>
        /// <param name="item6">The sixth item.</param>
        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
            : base(item1, item2, item3, item4, item5)
        {
            _item6 = item6;
        }

        /// <summary>
        /// Gets the value indicating the value of Item4.
        /// </summary>
        /// <value>The value of Item4.</value>
        public T6 Item6
        {
            get
            {
                return _item6;
            }
        }
    }

    /// <summary>
    /// Represents a data structure that has a specific number and sequence of values.
    /// </summary>
    [DebuggerStepThrough]
    public class Tuple<T1, T2, T3, T4, T5, T6, T7> : Tuple<T1, T2, T3, T4, T5, T6>, ITuple<T1, T2, T3, T4, T5, T6, T7>
    {
        private readonly T7 _item7;

        /// <summary>
        /// Initializes a new instance of the Tuple class.
        /// </summary>
        /// <param name="item1">The first item.</param>
        /// <param name="item2">The second item.</param>
        /// <param name="item3">The third item.</param>
        /// <param name="item4">The fourth item.</param>
        /// <param name="item5">The fifth item.</param>
        /// <param name="item6">The sixth item.</param>
        /// <param name="item7">The seventh item.</param>
        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
            : base(item1, item2, item3, item4, item5, item6)
        {
            _item7 = item7;
        }

        /// <summary>
        /// Gets the value indicating the value of Item4.
        /// </summary>
        /// <value>The value of Item4.</value>
        public T7 Item7
        {
            get
            {
                return _item7;
            }
        }
    }

    /// <summary>
    /// Represents a data structure that has a specific number and sequence of values.
    /// </summary>
    [DebuggerStepThrough]
    public class Tuple<T1, T2, T3, T4, T5, T6, T7, T8> : Tuple<T1, T2, T3, T4, T5, T6, T7>, ITuple<T1, T2, T3, T4, T5, T6, T7, T8>
    {
        private readonly T8 _item8;

        /// <summary>
        /// Initializes a new instance of the Tuple class.
        /// </summary>
        /// <param name="item1">The first item.</param>
        /// <param name="item2">The second item.</param>
        /// <param name="item3">The third item.</param>
        /// <param name="item4">The fourth item.</param>
        /// <param name="item5">The fifth item.</param>
        /// <param name="item6">The sixth item.</param>
        /// <param name="item7">The seventh item.</param>
        /// <param name="item8">The eighth item.</param>
        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
            : base(item1, item2, item3, item4, item5, item6, item7)
        {
            _item8 = item8;
        }

        /// <summary>
        /// Gets the value indicating the value of Item4.
        /// </summary>
        /// <value>The value of Item4.</value>
        public T8 Item8
        {
            get
            {
                return _item8;
            }
        }
    }

    /// <summary>
    /// Represents a data structure that has a specific number and sequence of values.
    /// </summary>
    [DebuggerStepThrough]
    public class Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> : Tuple<T1, T2, T3, T4, T5, T6, T7, T8>, ITuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        private readonly T9 _item9;

        /// <summary>
        /// Initializes a new instance of the Tuple class.
        /// </summary>
        /// <param name="item1">The first item.</param>
        /// <param name="item2">The second item.</param>
        /// <param name="item3">The third item.</param>
        /// <param name="item4">The fourth item.</param>
        /// <param name="item5">The fifth item.</param>
        /// <param name="item6">The sixth item.</param>
        /// <param name="item7">The seventh item.</param>
        /// <param name="item8">The eighth item.</param>
        /// <param name="item9">The ninth item.</param>
        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9)
            : base(item1, item2, item3, item4, item5, item6, item7, item8)
        {
            _item9 = item9;
        }

        /// <summary>
        /// Gets the value indicating the value of Item4.
        /// </summary>
        /// <value>The value of Item4.</value>        
        public T9 Item9
        {
            get
            {
                return _item9;
            }
        }
    }

    /// <summary>
    /// Represents an extension class that allows users to instantiate tuple objects.
    /// </summary>
    [DebuggerStepThrough]
    public static class Tuple
    {
        /// <summary>
        /// Creates an instance of a tuple class.
        /// </summary>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="item">The value of item1.</param>
        /// <returns>A tuple that contains the given items.</returns>
        [DebuggerStepThrough]
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
        [DebuggerStepThrough]
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
        [DebuggerStepThrough]
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
        [DebuggerStepThrough]
        public static ITuple<T1, T2, T3, T4> New<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            return new Tuple<T1, T2, T3, T4>(item1, item2, item3, item4);
        }

        /// <summary>
        /// Creates an instance of a tuple class.
        /// </summary>
        /// <typeparam name="T1">The item1 type.</typeparam>
        /// <typeparam name="T2">The item2 type.</typeparam>
        /// <typeparam name="T3">The item3 type.</typeparam>
        /// <typeparam name="T4">The item4 type.</typeparam>
        /// <typeparam name="T5">The item5 type.</typeparam>
        /// <param name="item1">The value of item1.</param>
        /// <param name="item2">The value of item2.</param>
        /// <param name="item3">The value of item3.</param>
        /// <param name="item4">The value of item4.</param>
        /// <param name="item5">The value of item5.</param>
        /// <returns>A tuple that contains the given items.</returns>
        [DebuggerStepThrough]
        public static ITuple<T1, T2, T3, T4, T5> New<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
        {
            return new Tuple<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5);
        }

        /// <summary>
        /// Creates an instance of a tuple class.
        /// </summary>
        /// <typeparam name="T1">The item1 type.</typeparam>
        /// <typeparam name="T2">The item2 type.</typeparam>
        /// <typeparam name="T3">The item3 type.</typeparam>
        /// <typeparam name="T4">The item4 type.</typeparam>
        /// <typeparam name="T5">The item5 type.</typeparam>
        /// <typeparam name="T6">The item6type.</typeparam>
        /// <param name="item1">The value of item1.</param>
        /// <param name="item2">The value of item2.</param>
        /// <param name="item3">The value of item3.</param>
        /// <param name="item4">The value of item4.</param>
        /// <param name="item5">The value of item5.</param>
        /// <param name="item6">The value of item6.</param>
        /// <returns>A tuple that contains the given items.</returns>
        [DebuggerStepThrough]
        public static ITuple<T1, T2, T3, T4, T5, T6> New<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6);
        }

        /// <summary>
        /// Creates an instance of a tuple class.
        /// </summary>
        /// <typeparam name="T1">The item1 type.</typeparam>
        /// <typeparam name="T2">The item2 type.</typeparam>
        /// <typeparam name="T3">The item3 type.</typeparam>
        /// <typeparam name="T4">The item4 type.</typeparam>
        /// <typeparam name="T5">The item5 type.</typeparam>
        /// <typeparam name="T6">The item6 type.</typeparam>
        /// <typeparam name="T7">The item7 type.</typeparam>
        /// <param name="item1">The value of item1.</param>
        /// <param name="item2">The value of item2.</param>
        /// <param name="item3">The value of item3.</param>
        /// <param name="item4">The value of item4.</param>
        /// <param name="item5">The value of item5.</param>
        /// <param name="item6">The value of item6.</param>
        /// <param name="item7">The value of item7.</param>
        /// <returns>A tuple that contains the given items.</returns>
        [DebuggerStepThrough]
        public static ITuple<T1, T2, T3, T4, T5, T6, T7> New<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7);
        }

        /// <summary>
        /// Creates an instance of a tuple class.
        /// </summary>
        /// <typeparam name="T1">The item1 type.</typeparam>
        /// <typeparam name="T2">The item2 type.</typeparam>
        /// <typeparam name="T3">The item3 type.</typeparam>
        /// <typeparam name="T4">The item4 type.</typeparam>
        /// <typeparam name="T5">The item5 type.</typeparam>
        /// <typeparam name="T6">The item6 type.</typeparam>
        /// <typeparam name="T7">The item7 type.</typeparam>
        /// <typeparam name="T8">The item8 type.</typeparam>
        /// <param name="item1">The value of item1.</param>
        /// <param name="item2">The value of item2.</param>
        /// <param name="item3">The value of item3.</param>
        /// <param name="item4">The value of item4.</param>
        /// <param name="item5">The value of item5.</param>
        /// <param name="item6">The value of item6.</param>
        /// <param name="item7">The value of item7.</param>
        /// <param name="item8">The value of item8.</param>
        /// <returns>A tuple that contains the given items.</returns>
        [DebuggerStepThrough]
        public static ITuple<T1, T2, T3, T4, T5, T6, T7, T8> New<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, T8>(item1, item2, item3, item4, item5, item6, item7, item8);
        }

        /// <summary>
        /// Creates an instance of a tuple class.
        /// </summary>
        /// <typeparam name="T1">The item1 type.</typeparam>
        /// <typeparam name="T2">The item2 type.</typeparam>
        /// <typeparam name="T3">The item3 type.</typeparam>
        /// <typeparam name="T4">The item4 type.</typeparam>
        /// <typeparam name="T5">The item5 type.</typeparam>
        /// <typeparam name="T6">The item6 type.</typeparam>
        /// <typeparam name="T7">The item7 type.</typeparam>
        /// <typeparam name="T8">The item8 type.</typeparam>
        /// <typeparam name="T9">The item9 type.</typeparam>
        /// <param name="item1">The value of item1.</param>
        /// <param name="item2">The value of item2.</param>
        /// <param name="item3">The value of item3.</param>
        /// <param name="item4">The value of item4.</param>
        /// <param name="item5">The value of item5.</param>
        /// <param name="item6">The value of item6.</param>
        /// <param name="item7">The value of item7.</param>
        /// <param name="item8">The value of item8.</param>
        /// <param name="item8">The value of item9.</param>
        /// <returns>A tuple that contains the given items.</returns>
        [DebuggerStepThrough]
        public static ITuple<T1, T2, T3, T4, T5, T6, T7, T8, T9> New<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>(item1, item2, item3, item4, item5, item6, item7, item8, item9);
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
        [DebuggerStepThrough]
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
        /// <param name="item4">The value of item4.</param>
        /// <returns>The value returned by the function.</returns>
        [DebuggerStepThrough]
        public static TOutput Execute<T1, T2, T3, T4, TOutput>(this IFunction<ITuple<T1, T2, T3, T4>, TOutput> function, T1 item1, T2 item2, T3 item3, T4 item4)
        {
            return function.Execute(Tuple.New(item1, item2, item3, item4));
        }

        /// <summary>
        /// Executes the given function using a tuple from the given arguments.
        /// </summary>
        /// <typeparam name="T1">The item1 type.</typeparam>
        /// <typeparam name="T2">The item2 type.</typeparam>
        /// <typeparam name="T3">The item3 type.</typeparam>
        /// <typeparam name="T4">The item4 type.</typeparam>
        /// <typeparam name="T5">The item5 type.</typeparam>
        /// <typeparam name="TOutput">The return type.</typeparam>
        /// <param name="function">The target function.</param>
        /// <param name="item1">The value of item1.</param>
        /// <param name="item2">The value of item2.</param>
        /// <param name="item3">The value of item3.</param>
        /// <param name="item4">The value of item4.</param>
        /// <param name="item5">The value of item5.</param>
        /// <returns>The value returned by the function.</returns>
        [DebuggerStepThrough]
        public static TOutput Execute<T1, T2, T3, T4, T5, TOutput>(this IFunction<ITuple<T1, T2, T3, T4, T5>, TOutput> function, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
        {
            return function.Execute(Tuple.New(item1, item2, item3, item4, item5));
        }

        /// <summary>
        /// Executes the given function using a tuple from the given arguments.
        /// </summary>
        /// <typeparam name="T1">The item1 type.</typeparam>
        /// <typeparam name="T2">The item2 type.</typeparam>
        /// <typeparam name="T3">The item3 type.</typeparam>
        /// <typeparam name="T4">The item4 type.</typeparam>
        /// <typeparam name="T5">The item5 type.</typeparam>
        /// <typeparam name="T6">The item6 type.</typeparam>
        /// <typeparam name="TOutput">The return type.</typeparam>
        /// <param name="function">The target function.</param>
        /// <param name="item1">The value of item1.</param>
        /// <param name="item2">The value of item2.</param>
        /// <param name="item3">The value of item3.</param>
        /// <param name="item4">The value of item4.</param>
        /// <param name="item5">The value of item5.</param>
        /// <param name="item6">The value of item6.</param>
        /// <returns>The value returned by the function.</returns>
        [DebuggerStepThrough]
        public static TOutput Execute<T1, T2, T3, T4, T5, T6, TOutput>(this IFunction<ITuple<T1, T2, T3, T4, T5, T6>, TOutput> function, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
        {
            return function.Execute(Tuple.New(item1, item2, item3, item4, item5, item6));
        }

        /// <summary>
        /// Executes the given function using a tuple from the given arguments.
        /// </summary>
        /// <typeparam name="T1">The item1 type.</typeparam>
        /// <typeparam name="T2">The item2 type.</typeparam>
        /// <typeparam name="T3">The item3 type.</typeparam>
        /// <typeparam name="T4">The item4 type.</typeparam>
        /// <typeparam name="T5">The item5 type.</typeparam>
        /// <typeparam name="T6">The item6 type.</typeparam>
        /// <typeparam name="T7">The item7 type.</typeparam>
        /// <typeparam name="TOutput">The return type.</typeparam>
        /// <param name="function">The target function.</param>
        /// <param name="item1">The value of item1.</param>
        /// <param name="item2">The value of item2.</param>
        /// <param name="item3">The value of item3.</param>
        /// <param name="item4">The value of item4.</param>
        /// <param name="item5">The value of item5.</param>
        /// <param name="item6">The value of item6.</param>
        /// <param name="item7">The value of item7.</param>
        /// <returns>The value returned by the function.</returns>
        [DebuggerStepThrough]
        public static TOutput Execute<T1, T2, T3, T4, T5, T6, T7, TOutput>(this IFunction<ITuple<T1, T2, T3, T4, T5, T6, T7>, TOutput> function, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
        {
            return function.Execute(Tuple.New(item1, item2, item3, item4, item5, item6, item7));
        }

        /// <summary>
        /// Executes the given function using a tuple from the given arguments.
        /// </summary>
        /// <typeparam name="T1">The item1 type.</typeparam>
        /// <typeparam name="T2">The item2 type.</typeparam>
        /// <typeparam name="T3">The item3 type.</typeparam>
        /// <typeparam name="T4">The item4 type.</typeparam>
        /// <typeparam name="T5">The item5 type.</typeparam>
        /// <typeparam name="T6">The item6 type.</typeparam>
        /// <typeparam name="T7">The item7 type.</typeparam>
        /// <typeparam name="T8">The item8 type.</typeparam>
        /// <typeparam name="TOutput">The return type.</typeparam>
        /// <param name="function">The target function.</param>
        /// <param name="item1">The value of item1.</param>
        /// <param name="item2">The value of item2.</param>
        /// <param name="item3">The value of item3.</param>
        /// <param name="item4">The value of item4.</param>
        /// <param name="item5">The value of item5.</param>
        /// <param name="item6">The value of item6.</param>
        /// <param name="item7">The value of item7.</param>
        /// <param name="item8">The value of item8.</param>
        /// <returns>The value returned by the function.</returns>
        [DebuggerStepThrough]
        public static TOutput Execute<T1, T2, T3, T4, T5, T6, T7, T8, TOutput>(this IFunction<ITuple<T1, T2, T3, T4, T5, T6, T7, T8>, TOutput> function, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
        {
            return function.Execute(Tuple.New(item1, item2, item3, item4, item5, item6, item7, item8));
        }

        /// <summary>
        /// Executes the given function using a tuple from the given arguments.
        /// </summary>
        /// <typeparam name="T1">The item1 type.</typeparam>
        /// <typeparam name="T2">The item2 type.</typeparam>
        /// <typeparam name="T3">The item3 type.</typeparam>
        /// <typeparam name="T4">The item4 type.</typeparam>
        /// <typeparam name="T5">The item5 type.</typeparam>
        /// <typeparam name="T6">The item6 type.</typeparam>
        /// <typeparam name="T7">The item7 type.</typeparam>
        /// <typeparam name="T8">The item8 type.</typeparam>
        /// <typeparam name="T9">The item9 type.</typeparam>
        /// <typeparam name="TOutput">The return type.</typeparam>
        /// <param name="function">The target function.</param>
        /// <param name="item1">The value of item1.</param>
        /// <param name="item2">The value of item2.</param>
        /// <param name="item3">The value of item3.</param>
        /// <param name="item4">The value of item4.</param>
        /// <param name="item5">The value of item5.</param>
        /// <param name="item6">The value of item6.</param>
        /// <param name="item7">The value of item7.</param>
        /// <param name="item8">The value of item8.</param>
        /// <param name="item9">The value of item9.</param>
        /// <returns>The value returned by the function.</returns>
        [DebuggerStepThrough]
        public static TOutput Execute<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOutput>(this IFunction<ITuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>, TOutput> function, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9)
        {
            return function.Execute(Tuple.New(item1, item2, item3, item4, item5, item6, item7, item8, item9));
        }

        /// <summary>
        /// Executes the given function using a tuple from the given arguments.
        /// </summary>
        /// <typeparam name="T1">The item1 type.</typeparam>
        /// <typeparam name="T2">The item2 type.</typeparam>
        /// <param name="function">The target function.</param>
        /// <param name="item1">The value of item1.</param>
        /// <param name="item2">The value of item2.</param>
        [DebuggerStepThrough]
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
        [DebuggerStepThrough]
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
        [DebuggerStepThrough]
        public static void Execute<T1, T2, T3, T4>(this IFunction<ITuple<T1, T2, T3, T4>> function, T1 item1, T2 item2, T3 item3, T4 item4)
        {
            function.Execute(Tuple.New(item1, item2, item3, item4));
        }

        /// <summary>
        /// Executes the given function using a tuple from the given arguments.
        /// </summary>
        /// <typeparam name="T1">The item1 type.</typeparam>
        /// <typeparam name="T2">The item2 type.</typeparam>
        /// <typeparam name="T3">The item3 type.</typeparam>
        /// <typeparam name="T4">The item4 type.</typeparam>
        /// <typeparam name="T5">The item5 type.</typeparam>
        /// <param name="function">The target function.</param>
        /// <param name="item1">The value of item1.</param>
        /// <param name="item2">The value of item2.</param>
        /// <param name="item3">The value of item3.</param>
        /// <param name="item4">The value of item4.</param>
        /// <param name="item5">The value of item5.</param>
        /// <returns>The value returned by the function.</returns>
        [DebuggerStepThrough]
        public static void Execute<T1, T2, T3, T4, T5>(this IFunction<ITuple<T1, T2, T3, T4, T5>> function, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
        {
            function.Execute(Tuple.New(item1, item2, item3, item4, item5));
        }

        /// <summary>
        /// Executes the given function using a tuple from the given arguments.
        /// </summary>
        /// <typeparam name="T1">The item1 type.</typeparam>
        /// <typeparam name="T2">The item2 type.</typeparam>
        /// <typeparam name="T3">The item3 type.</typeparam>
        /// <typeparam name="T4">The item4 type.</typeparam>
        /// <typeparam name="T5">The item5 type.</typeparam>
        /// <typeparam name="T6">The item6 type.</typeparam>
        /// <param name="function">The target function.</param>
        /// <param name="item1">The value of item1.</param>
        /// <param name="item2">The value of item2.</param>
        /// <param name="item3">The value of item3.</param>
        /// <param name="item4">The value of item4.</param>
        /// <param name="item5">The value of item5.</param>
        /// <param name="item6">The value of item6.</param>
        /// <returns>The value returned by the function.</returns>
        [DebuggerStepThrough]
        public static void Execute<T1, T2, T3, T4, T5, T6>(this IFunction<ITuple<T1, T2, T3, T4, T5, T6>> function, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
        {
            function.Execute(Tuple.New(item1, item2, item3, item4, item5, item6));
        }

        /// <summary>
        /// Executes the given function using a tuple from the given arguments.
        /// </summary>
        /// <typeparam name="T1">The item1 type.</typeparam>
        /// <typeparam name="T2">The item2 type.</typeparam>
        /// <typeparam name="T3">The item3 type.</typeparam>
        /// <typeparam name="T4">The item4 type.</typeparam>
        /// <typeparam name="T5">The item5 type.</typeparam>
        /// <typeparam name="T6">The item6 type.</typeparam>
        /// <typeparam name="T7">The item7 type.</typeparam>
        /// <param name="function">The target function.</param>
        /// <param name="item1">The value of item1.</param>
        /// <param name="item2">The value of item2.</param>
        /// <param name="item3">The value of item3.</param>
        /// <param name="item4">The value of item4.</param>
        /// <param name="item5">The value of item5.</param>
        /// <param name="item6">The value of item6.</param>
        /// <param name="item7">The value of item7.</param>
        /// <returns>The value returned by the function.</returns>
        [DebuggerStepThrough]
        public static void Execute<T1, T2, T3, T4, T5, T6, T7>(this IFunction<ITuple<T1, T2, T3, T4, T5, T6, T7>> function, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
        {
            function.Execute(Tuple.New(item1, item2, item3, item4, item5, item6, item7));
        }

        /// <summary>
        /// Executes the given function using a tuple from the given arguments.
        /// </summary>
        /// <typeparam name="T1">The item1 type.</typeparam>
        /// <typeparam name="T2">The item2 type.</typeparam>
        /// <typeparam name="T3">The item3 type.</typeparam>
        /// <typeparam name="T4">The item4 type.</typeparam>
        /// <typeparam name="T5">The item5 type.</typeparam>
        /// <typeparam name="T6">The item6 type.</typeparam>
        /// <typeparam name="T7">The item7 type.</typeparam>
        /// <typeparam name="T8">The item8 type.</typeparam>
        /// <param name="function">The target function.</param>
        /// <param name="item1">The value of item1.</param>
        /// <param name="item2">The value of item2.</param>
        /// <param name="item3">The value of item3.</param>
        /// <param name="item4">The value of item4.</param>
        /// <param name="item5">The value of item5.</param>
        /// <param name="item6">The value of item6.</param>
        /// <param name="item7">The value of item7.</param>
        /// <param name="item8">The value of item8.</param>
        /// <returns>The value returned by the function.</returns>
        [DebuggerStepThrough]
        public static void Execute<T1, T2, T3, T4, T5, T6, T7, T8>(this IFunction<ITuple<T1, T2, T3, T4, T5, T6, T7, T8>> function, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
        {
            function.Execute(Tuple.New(item1, item2, item3, item4, item5, item6, item7, item8));
        }

        /// <summary>
        /// Executes the given function using a tuple from the given arguments.
        /// </summary>
        /// <typeparam name="T1">The item1 type.</typeparam>
        /// <typeparam name="T2">The item2 type.</typeparam>
        /// <typeparam name="T3">The item3 type.</typeparam>
        /// <typeparam name="T4">The item4 type.</typeparam>
        /// <typeparam name="T5">The item5 type.</typeparam>
        /// <typeparam name="T6">The item6 type.</typeparam>
        /// <typeparam name="T7">The item7 type.</typeparam>
        /// <typeparam name="T8">The item8 type.</typeparam>
        /// <typeparam name="T9">The item9 type.</typeparam>
        /// <param name="function">The target function.</param>
        /// <param name="item1">The value of item1.</param>
        /// <param name="item2">The value of item2.</param>
        /// <param name="item3">The value of item3.</param>
        /// <param name="item4">The value of item4.</param>
        /// <param name="item5">The value of item5.</param>
        /// <param name="item6">The value of item6.</param>
        /// <param name="item7">The value of item7.</param>
        /// <param name="item8">The value of item8.</param>
        /// <param name="item9">The value of item9.</param>
        /// <returns>The value returned by the function.</returns>
        [DebuggerStepThrough]
        public static void Execute<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IFunction<ITuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>> function, T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9)
        {
            function.Execute(Tuple.New(item1, item2, item3, item4, item5, item6, item7, item8, item9));
        }
    }
}