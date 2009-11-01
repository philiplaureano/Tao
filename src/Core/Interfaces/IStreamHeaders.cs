using System.Collections.Generic;

namespace Tao.Core
{
    /// <summary>
    /// Represents a set of metadata stream headers within a .NET portable executable image.
    /// </summary>
    public interface IStreamHeaders : IList<IStreamHeader>, IHeader
    {
    }
}