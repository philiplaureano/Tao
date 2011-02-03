namespace Tao.Model
{
    public class BaseVarSignature : TypeSignature
    {
        /// <summary>
        /// Gets the value indicating the argument index for the current generic method type argument.
        /// </summary>
        /// <value>The argument index for the current generic method type argument.</value>
        public uint ArgumentIndex { get; set; }
    }
}