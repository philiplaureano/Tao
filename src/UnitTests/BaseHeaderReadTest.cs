using System.IO;

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
    }
}