using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao.Model
{
    /// <summary>
    /// Represents a MethodSpec signature.
    /// </summary>
    public class MethodSpec
    {
        private readonly List<TypeSignature> _typeParameters = new List<TypeSignature>();

        /// <summary>
        /// Gets or sets the value indicating the generic type definition that will be used
        /// to instantiate the method spec instance.
        /// </summary>
        /// <value>The generic type definition signature that will be used to instantiate the method spec.</value>
        public TypeSignature GenericTypeDefinition
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value indicating the type parameters that will be used to instantiate the 
        /// method spec.
        /// </summary>
        /// <value>The generic type parameters.</value>
        public IList<TypeSignature> TypeParameters
        {
            get { return _typeParameters; }
        }
    }
}
