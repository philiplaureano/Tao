using System;
using System.Collections.Generic;
using System.Text;
using Hiro.Containers;
using Tao.Interfaces;

namespace Tao.Core
{
    /// <summary>
    /// Represents a factory instance that converts a functor to a factory type.
    /// </summary>
    /// <typeparam name="TResult">The result type.</typeparam>
    internal class FunctorFactoryAdapter<TResult> : IFactory<TResult>
    {
        private readonly Func<TResult> _functor;

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctorFactoryAdapter{TResult}"/> class.
        /// </summary>
        /// <param name="functor">The factory functor.</param>
        public FunctorFactoryAdapter(Func<TResult> functor)
        {
            _functor = functor;
        }

        /// <summary>
        /// Creates the given <typeparamref name="TResult"/> type.
        /// </summary>
        /// <returns>The <typeparamref name="TResult"/> instance.</returns>
        public TResult Create()
        {
            return _functor();
        }
    }
}
