using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Interfaces
{
    /// <summary>
    /// Represents a <see cref="IFactory{TResult}"/> instance that takes a single parameter upon creating the <see cref="TResult"/> type.
    /// </summary>
    /// <typeparam name="TInput">The input type.</typeparam>
    /// <typeparam name="TResult">The result type.</typeparam>
    public interface IAdaptedFactory<TInput, TResult> : IFactory<TResult>
    {
        /// <summary>
        /// Gets or sets the value indicating the input value for the current factory.
        /// </summary>
        /// <value>The input value for the current factory.</value>
        TInput Input { get; set; }
    }
}
