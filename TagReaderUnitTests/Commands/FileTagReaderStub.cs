using TagReader;
using TagReader.FileTagReader;

namespace TagReaderUnitTests.Commands
{
    public class FileTagReaderStub : IFileTagReader
    {
        public bool IsFileSupported
        {
            get; 
            set;
        }

        public FileTag Tag
        {
            get; 
            set;
        }

        public FileTag Read(string filePath)
        {
            return Tag;
        }

        public void SaveTag(FileTag tag)
        {
            Tag = tag;
        }

        public bool IsFileFormatSupported(string filePath)
        {
            return IsFileSupported;
        }
    }
}