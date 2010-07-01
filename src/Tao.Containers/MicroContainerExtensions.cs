using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hiro.Containers;

namespace Tao.Containers
{
    /// <summary>
    /// Represents a class that adds syntactic sugar to Hiro's microcontainer instances.
    /// </summary>
    public static class MicroContainerExtensions
    {
        /// <summary>
        /// Obtains an object instance that matches the given service type.
        /// </summary>
        /// <typeparam name="T">The service type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>An object instance that matches the given service description.</returns>
        public static T GetInstance<T>(this IMicroContainer container)
        {
            return container.GetInstance<T>(null);
        }

        /// <summary>
        /// Obtains an object instance that matches the given service type
        /// and <paramref name="key">service name</paramref>.
        /// </summary>
        /// <typeparam name="T">The service type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="key">The name of the service itself.</param>
        /// <returns>An object instance that matches the given service description.</returns>
        public static T GetInstance<T>(this IMicroContainer container, string key)
        {
            return (T)container.GetInstance(typeof(T), key);
        }

        /// <summary>
        /// Determines whetehr or not a particular service exists within a container.
        /// </summary>
        /// <typeparam name="T">The service type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>An object instance that matches the given service description.</returns>
        public static bool Contains<T>(this IMicroContainer container)
        {
            return container.Contains(typeof(T), null);
        }

        /// <summary>
        /// Determines whetehr or not a particular service exists within a container.
        /// </summary>
        /// <typeparam name="T">The service type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="key">The name of the service itself.</param>
        /// <returns>An object instance that matches the given service description.</returns>
        public static bool Contains<T>(this IMicroContainer container, string key)
        {
            return container.Contains(typeof (T), key);
        }
    }
}
