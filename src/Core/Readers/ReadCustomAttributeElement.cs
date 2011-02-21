using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads custom attribute elements from a CustomAttrib signature.
    /// </summary>
    public class ReadCustomAttributeElement : IFunction<ITuple<ElementType, Stream>, object>
    {
        private readonly IFunction<Stream, bool> _readBoolean;
        private readonly IFunction<Stream, byte> _readByte;
        private readonly IFunction<Stream, char> _readChar;
        private readonly IFunction<Stream, float> _readSingle;
        private readonly IFunction<Stream, double> _readDouble;
        private readonly IFunction<Stream, Int16> _readInt16;
        private readonly IFunction<Stream, Int32> _readInt32;
        private readonly IFunction<Stream, Int64> _readInt64;
        private readonly IFunction<Stream, sbyte> _readSByte;
        private readonly IFunction<Stream, UInt16> _readUInt16;
        private readonly IFunction<Stream, UInt32> _readUInt32;
        private readonly IFunction<Stream, UInt64> _readUInt64;
        private readonly IFunction<Stream, string> _readSerString;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadCustomAttributeElement"/> class.
        /// </summary>
        /// <param name="readBoolean">The boolean reader.</param>
        /// <param name="readByte">The byte reader.</param>
        /// <param name="readChar">The char reader.</param>
        /// <param name="readSingle">The float reader.</param>
        /// <param name="readDouble">The double reader.</param>
        /// <param name="readInt16">The short reader.</param>
        /// <param name="readInt32">The double word reader.</param>
        /// <param name="readInt64">The quad word reader.</param>
        /// <param name="readSByte">The signed byte reader.</param>
        /// <param name="readUInt16">The unsigned word reader.</param>
        /// <param name="readUInt32">The unsigned double word reader.</param>
        /// <param name="readUInt64">The unsigned quad word reader.</param>
        /// <param name="readSerString">The string reader.</param>
        public ReadCustomAttributeElement(IFunction<Stream, bool> readBoolean, 
            IFunction<Stream, byte> readByte, 
            IFunction<Stream, char> readChar, 
            IFunction<Stream, float> readSingle, 
            IFunction<Stream, double> readDouble, 
            IFunction<Stream, short> readInt16, 
            IFunction<Stream, int> readInt32, 
            IFunction<Stream, long> readInt64, 
            IFunction<Stream, sbyte> readSByte, 
            IFunction<Stream, ushort> readUInt16, 
            IFunction<Stream, uint> readUInt32, 
            IFunction<Stream, ulong> readUInt64, 
            IFunction<Stream, string> readSerString)
        {
            _readBoolean = readBoolean;
            _readSerString = readSerString;
            _readUInt64 = readUInt64;
            _readUInt32 = readUInt32;
            _readUInt16 = readUInt16;
            _readSByte = readSByte;
            _readInt64 = readInt64;
            _readInt32 = readInt32;
            _readInt16 = readInt16;
            _readDouble = readDouble;
            _readSingle = readSingle;
            _readChar = readChar;
            _readByte = readByte;
        }

        /// <summary>
        /// Reads an element from the given type using the given <see cref="ElementType"/> and input <see cref="Stream"/>.
        /// </summary>
        /// <param name="input">The given <see cref="ElementType"/> and input <see cref="Stream"/>.</param>
        /// <returns>Returns the element value.</returns>
        public object Execute(ITuple<ElementType, Stream> input)
        {
            var elementType = input.Item1;
            var stream = input.Item2;

            if (elementType == ElementType.Boolean)
                return _readBoolean.Execute(stream);

            if (elementType == ElementType.U1)
                return _readByte.Execute(stream);

            if (elementType == ElementType.Char)
                return _readChar.Execute(stream);

            if (elementType == ElementType.R4)
                return _readSingle.Execute(stream);

            if (elementType == ElementType.R8)
                return _readDouble.Execute(stream);

            if (elementType == ElementType.I2)
                return _readInt16.Execute(stream);

            if (elementType == ElementType.I4)
                return _readInt32.Execute(stream);

            if (elementType == ElementType.I8)
                return _readInt64.Execute(stream);

            if (elementType == ElementType.I1)
                return _readSByte.Execute(stream);

            if (elementType == ElementType.U2)
                return _readUInt16.Execute(stream);

            if (elementType == ElementType.U4)
                return _readUInt32.Execute(stream);

            if (elementType == ElementType.U8)
                return _readUInt64.Execute(stream);

            if (elementType == ElementType.String)
                return _readSerString.Execute(stream);
            
            throw new NotSupportedException(string.Format("Element type not supported: '{0}'", elementType));
        }
    }
}
