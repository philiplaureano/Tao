using System;
using System.Collections.Generic;
using System.Text;
using Hiro.Containers;
using Tao.Interfaces;

namespace Tao.Core
{
    /// <summary>
    /// Represents a type that can convert functors into factory instances.
    /// </summary>
    public class FunctorFactoryConverter : IFunctorFactoryConverter
    {
        /// <summary>
        /// Converts a functor to a factory type.
        /// </summary>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <param name="functor">The factory functor.</param>
        /// <returns>Returns an <see cref="IFactory{T}"/> instance.</returns>
        public IFactory<TResult> ConvertFrom<TResult>(Func<TResult> functor)
        {
            return new FunctorFactoryAdapter<TResult>(functor);
        }
    }
}
