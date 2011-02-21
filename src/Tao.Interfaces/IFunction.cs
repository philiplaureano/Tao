using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Interfaces
{
    /// <summary>
    /// Represents a converter type.
    /// </summary>
    /// <typeparam name="TInput">The input type that will be used to create the object.</typeparam>
    /// <typeparam name="TOutput">The type of object that will be created by the factory itself.</typeparam>
    public interface IFunction<in TInput, out TOutput>
    {
        /// <summary>
        /// Creates the given <typeparamref name="TOutput"/> using the given <typeparamref name="TInput"/>.
        /// </summary>
        /// <param name="input">The input data.</param>
        /// <returns>The <typeparamref name="TOutput"/> instance.</returns>
        TOutput Execute(TInput input);
    }

    /// <summary>
    /// Represents a converter type.
    /// </summary>
    /// <typeparam name="TInput">The input type that will be used to create the object.</typeparam>
    public interface IFunction<in TInput>
    {
        /// <summary>
        /// Executes the given action using the given <typeparamref name="TInput"/>.
        /// </summary>
        /// <param name="input">The input data.</param
        void Execute(TInput input);
    }
}
