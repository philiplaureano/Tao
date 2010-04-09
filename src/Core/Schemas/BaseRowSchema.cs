using System;
using Tao.Interfaces;

namespace Tao.Core.Schemas
{
    /// <summary>
    /// Represents the basic implementation of a row schema class.
    /// </summary>
    public abstract class BaseRowSchema : ITuple<int, int, int, int, int>
    {
        private readonly int _wordColumns;
        private readonly int _doubleWordColumns;
        private readonly int _stringHeapColumns;
        private readonly int _blobHeapColumns;
        private readonly int _guidHeapColumns;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public BaseRowSchema(int wordColumns, int doubleWordColumns, int stringHeapColumns, int blobHeapColumns, int guidHeapColumns)
        {
            _wordColumns = wordColumns;
            _doubleWordColumns = doubleWordColumns;
            _stringHeapColumns = stringHeapColumns;
            _blobHeapColumns = blobHeapColumns;
            _guidHeapColumns = guidHeapColumns;
        }

        /// <summary>
        /// Gets the value indicating the number of word-sized columns.
        /// </summary>
        /// <value>The number of word-sized columns..</value>
        public int Item1
        {
            get
            {
                return _wordColumns;
            }
        }

        /// <summary>
        /// Gets the value indicating the number of double-word sized columns
        /// </summary>
        /// <value>The number of double-word sized columns.</value>
        public int Item2
        {
            get
            {
                return _doubleWordColumns;
            }
        }

        /// <summary>
        /// Gets the value indicating the number of columns that point to the #String heap.
        /// </summary>
        /// <value>The number of columns that point to the #String heap..</value>
        public int Item3
        {
            get
            {
                return _stringHeapColumns;
            }
        }

        /// <summary>
        /// Gets the value indicating the number of columns that point to the #Blob heap.
        /// </summary>
        /// <value>the number of columns that point to the #Blob heap.</value>
        public int Item4
        {
            get
            {
                return _blobHeapColumns;
            }
        }

        /// <summary>
        /// Gets the value indicating the number of columns that point to the #Guid heap.
        /// </summary>
        /// <value>the number of columns that point to the #Guid heap.</value>
        public int Item5
        {
            get
            {
                return _guidHeapColumns;
            }
        }
    }
}