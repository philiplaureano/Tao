using System.Collections.Generic;

namespace Tao.Model
{
    /// <summary>
    /// Represents a field signature type.
    /// </summary>
    public class FieldSignature
    {
        private readonly List<CustomMod> _customMods = new List<CustomMod>();

        /// <summary>
        /// Gets or sets the value indicating the current <see cref="TypeSignature"/> of the current field signature instance.
        /// </summary>
        /// <value>The field type.</value>
        public TypeSignature FieldType { get; set; }

        /// <summary>
        /// Gets the value indicating the list of <see cref="CustomMods"/> associated with the current signature.
        /// </summary>
        /// <value>The current list of custom mods.</value>
        public List<CustomMod> CustomMods
        {
            get { return _customMods; }
        }
    }
}
