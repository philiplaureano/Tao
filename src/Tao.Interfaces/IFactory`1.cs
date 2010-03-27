using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Interfaces
{
    /// <summary>
    /// Represents a factory type.
    /// </summary>
    /// <typeparam name="TResult">The type of object that will be created by the factory itself.</typeparam>
    public interface IFactory<TResult>
    {
        /// <summary>
        /// Creates the given <typeparamref name="TResult"/> type.
        /// </summary>
        /// <returns>The <typeparamref name="TResult"/> instance.</returns>
        TResult Create();
    }
}
