using System.Collections.Generic;

namespace Tao.Model
{
    public class PointerSignature : TypeSignature
    {
        private readonly IList<CustomMod> _customMods = new List<CustomMod>();

        /// <summary>
        /// Gets the value indicating the list of <see cref="CustomMod"/> instances
        /// associated with the current signature.
        /// </summary>
        /// <value>The list of custom mods.</value>
        public IList<CustomMod> CustomMods
        {
            get { return _customMods; }
        }
    }
}