using System.Collections.Generic;

namespace Tao.Model
{
    /// <summary>
    /// Represents an ArrayShape signature element.
    /// </summary>
    public class ArrayShape
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayShape"/> class.
        /// </summary>
        /// <param name="rank">The value indicating the number of dimensions in an array.</param>
        /// <param name="numSizes">The value indicating how many dimensions have specified lower bounds.</param>
        /// <param name="sizes">The value indicating the compressed integers that specify the size of each dimension.</param>
        /// <param name="loBounds">The value indicating the compressed integers that specify the lower bound of each dimension.</param>
        public ArrayShape(uint rank, uint numSizes, IEnumerable<uint> sizes, IEnumerable<uint> loBounds)
        {
            Rank = rank;
            NumSizes = numSizes;
            Sizes = sizes;
            LoBounds = loBounds;
        }

        /// <summary>
        /// Gets or sets the value indicating the number of dimensions in an array.
        /// </summary>
        /// <value>The number of dimensions in a given array.</value>
        public uint Rank { get; set; }

        /// <summary>
        /// Gets the value indicating how many dimensions have specified lower bounds.
        /// </summary>
        /// <value>The number of dimensions that have specified lower bounds.</value>
        public uint NumSizes { get; set; }

        /// <summary>
        /// Gets or sets the value indicating the compressed integers that specify the size of each dimension.
        /// </summary>
        /// <value>The value indicating the compressed integers that specify the size of each dimension</value>
        public IEnumerable<uint> Sizes { get; private set; }

        /// <summary>
        /// Gets or sets the value indicating the compressed integers that specify the lower bound of each dimension.
        /// </summary>
        /// <value>The value indicating the compressed integers that specify the lower bound of each dimension</value>
        public IEnumerable<uint> LoBounds { get; private set; }
    }
}
