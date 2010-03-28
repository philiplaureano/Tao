using System;
using System.Collections.Generic;
using System.Text;
using Hiro.Containers;
namespace Tao.Interfaces
{
    /// <summary>
    /// Represents a type that can convert one factory type to another factory type.
    /// </summary>
    public interface IFactoryConverter
    {
        /// <summary>
        /// Converts a <see cref="IFactory{TInput,TResult}"/> instance into an <see cref="IFactory{TResult}"/> instance.
        /// </summary>
        /// <typeparam name="TInput">The input type.</typeparam>
        /// <typeparam name="TResult">The result type.</typeparam>
        /// <param name="sourceFactory">The source factory instance.</param>
        /// <param name="input">The input value.</param>
        /// <returns>A <see cref="IFactory{TResult}"/> instance that represents the original <paramref name="sourceFactory"/>.</returns>
        IAdaptedFactory<TInput, TResult> Convert<TInput, TResult>(IConversion<TInput, TResult> sourceFactory, TInput input);
    }
}
