using System;
using System.IO;
using NUnit.Framework;
using Tao.Core;
using BinaryReader=Tao.Core.BinaryReader;

namespace Tao.UnitTests
{
    public abstract class BaseHeaderReadTest
    {
        protected readonly string _inputFile;
        protected readonly string _fullPath;

        protected BaseHeaderReadTest()
        {
            _inputFile = "skeleton.exe";
            _fullPath = Path.GetFullPath(_inputFile);
        }

        protected virtual FileStream OpenSampleAssembly()
        {
            var file = new FileStream(_fullPath, FileMode.Open, FileAccess.Read);

            return file;
        }

        protected void AssertEquals<TTarget, TValue>(TValue expected, Func<TTarget, TValue> getActualValue)
            where TTarget : IHeader, new()
        {
            Test<TTarget>(header => Assert.AreEqual(expected, getActualValue(header)));
        }

        private void Test<TTarget>(Action<TTarget> doTest)
            where TTarget : IHeader, new()
        {
            var stream = OpenSampleAssembly();
            SetStreamPosition(stream);

            var reader = new BinaryReader(stream);

            var header = new TTarget();
            header.ReadFrom(reader);

            doTest(header);
        }

        protected abstract void SetStreamPosition(Stream stream);        
    }
}