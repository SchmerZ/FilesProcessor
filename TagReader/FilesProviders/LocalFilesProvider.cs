using System.IO;

namespace TagReader.FilesProviders
{
    public class LocalFilesProvider : IFilesProvider
    {
        public bool DirectoryExists(string folderPath)
        {
            return Directory.Exists(folderPath);
        }

        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public string[] GetDirectories(string folderPath)
        {
            return Directory.GetDirectories(folderPath);
        }

        public string[] GetFiles(string folderPath)
        {
            return Directory.GetFiles(folderPath);
        }

        public void MoveFile(string sourceFilePath, string destinationFilePath)
        {
            File.Move(sourceFilePath, destinationFilePath);
        }
    }
}