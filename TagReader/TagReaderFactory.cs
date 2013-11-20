using TagReader.FileTagReader;

namespace TagReader
{
    public static class TagReaderFactory
    {
        private static IFileTagReader _fileTagReader = null;

        public static IFileTagReader GetFileTagReader()
        {
            return _fileTagReader ?? (_fileTagReader = new TagLibTagReaderWrapper());
        }
    }
}