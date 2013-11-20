namespace TagReader.FilesProviders
{
    public interface IFilesProvider
    {
        bool DirectoryExists(string folderPath);
        
        bool FileExists(string filePath);

        string[] GetDirectories(string folderPath);

        string[] GetFiles(string folderPath);

        void MoveFile(string sourceFilePath, string destinationFilePath);
    }
}