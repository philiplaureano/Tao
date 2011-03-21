using System;
using System.IO;
using Tao.Interfaces;

namespace Tao
{
    /// <summary>
    /// Represents a class that converts a relative virtual address into an absolute file position.
    /// </summary>
    public class ResolveAbsoluteFilePositionFromRva : IFunction<ITuple<int, Stream>, int>
    {
        private readonly IFunction<Stream, int> _readSectionCount;
        private readonly IFunction<ITuple<int, Stream>, Stream> _peSectionFactory;
        private readonly IFunction<Stream> _optionalHeaderSeeker;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolveAbsoluteFilePositionFromRva"/> class.
        /// </summary>
        public ResolveAbsoluteFilePositionFromRva(IFunction<Stream, int> readSectionCount, IFunction<ITuple<int, Stream>, Stream> peSectionFactory, IFunction<Stream> optionalHeaderSeeker)
        {
            _peSectionFactory = peSectionFactory;
            _optionalHeaderSeeker = optionalHeaderSeeker;
            _readSectionCount = readSectionCount;
        }

        /// <summary>
        /// Converts a relative virtual address into an absolute file position.
        /// </summary>
        /// <param name="input">The tuple that contains the RVA and the target stream.</param>
        /// <returns>The absolute file position of the RVA.</returns>
        public int Execute(ITuple<int, Stream> input)
        {
            var stream = input.Item2;
            var rva = input.Item1;

            stream.Seek(0x84, SeekOrigin.Begin);

            // Determine the size of the optional header
            stream.Seek(0x10, SeekOrigin.Current);
            var reader = new BinaryReader(stream);
            var optionalHeaderSize = reader.ReadInt16();

            // Move to the end of the optional header stream
            _optionalHeaderSeeker.Execute(stream);
            stream.Seek(optionalHeaderSize, SeekOrigin.Current);

            var sectionCount = _readSectionCount.Execute(stream);
            for (var i = 0; i < sectionCount; i++)
            {
                var sectionStream = _peSectionFactory.Execute(i, stream);
                var sectionReader = new BinaryReader(sectionStream);

                // Get the Section RVA
                sectionStream.Seek(0xC, SeekOrigin.Begin);
                var sectionRva = sectionReader.ReadUInt32();
                var sizeOfRawData = sectionReader.ReadUInt32();
                var pointerToRawData = sectionReader.ReadUInt32();

                var sectionBoundary = sectionRva + sizeOfRawData;

                if (rva < sectionRva || rva >= sectionBoundary) 
                    continue;

                var blockSize = rva - sectionRva;
                var result = pointerToRawData + blockSize;
                return (int) result;
            }

            return 0;
        }
    }
}
