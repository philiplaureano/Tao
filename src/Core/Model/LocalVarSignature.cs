using System.Collections.Generic;

namespace Tao.Model
{
    /// <summary>
    /// Represents a LocalVarSig signature.
    /// </summary>
    public class LocalVarSignature
    {
        private readonly List<LocalVariable> _localVariables = new List<LocalVariable>();

        /// <summary>
        /// Gets the value indicating the list of local variables associated with the current signature.
        /// </summary>
        /// <value>The list of local variables associated with the current signature.</value>
        public List<LocalVariable> LocalVariables
        {
            get { return _localVariables; }            
        }
    }
}
