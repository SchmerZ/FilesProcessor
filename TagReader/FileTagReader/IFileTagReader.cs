namespace TagReader.FileTagReader
{
    public interface IFileTagReader
    {
        FileTag Read(string filePath);

        void SaveTag(FileTag tag);

        bool IsFileFormatSupported(string filePath);
    }
}