using System;
using System.Collections.Generic;
using System.Text;
using Hiro.Containers;

namespace Tao.Interfaces
{
    /// <summary>
    /// Represents a type that can convert functors into factory instances.
    /// </summary>
    public interface IFunctorFactoryConverter
    {
        /// <summary>
        /// Converts a functor to a factory type.
        /// </summary>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <param name="functor">The factory functor.</param>
        /// <returns>Returns an <see cref="IFactory{TResult}"/> instance.</returns>
        IFactory<TResult> ConvertFrom<TResult>(Func<TResult> functor);
    }
}
