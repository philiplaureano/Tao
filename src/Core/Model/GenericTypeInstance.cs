using System.Collections.Generic;

namespace Tao.Model
{
    /// <summary>
    /// Represents a generic type instantiation.
    /// </summary>
    public class GenericTypeInstance : TypeSignature, ITypeSpecification
    {
        private readonly List<TypeSignature> _typeParameters = new List<TypeSignature>();

        /// <summary>
        /// Gets or sets the value indicating the generic type definition that will be used
        /// to instantiate the generic type instance.
        /// </summary>
        /// <value>The generic type definition signature that will be used to instantiate the generic type instance.</value>
        public TypeDefOrRefEncodedSignature GenericTypeDefinition
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the value indicating the type parameters that will be used to instantiate the 
        /// generic type instance.
        /// </summary>
        /// <value>The generic type parameters.</value>
        public IList<TypeSignature> TypeParameters
        {
            get { return _typeParameters; }
        }
    }
}
