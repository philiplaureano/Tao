using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Interfaces
{
    /// <summary>
    /// Represents a converter type.
    /// </summary>
    /// <typeparam name="TInput">The input type that will be used to create the object.</typeparam>
    /// <typeparam name="TResult">The type of object that will be created by the factory itself.</typeparam>
    public interface IConversion<TInput, TResult>
    {
        /// <summary>
        /// Creates the given <typeparamref name="TResult"/> using the given <typeparamref name="TInput"/>.
        /// </summary>
        /// <param name="input">The input data.</param>
        /// <returns>The <typeparamref name="TResult"/> instance.</returns>
        TResult Create(TInput input);
    }
}
