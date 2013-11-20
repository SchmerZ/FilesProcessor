using System;

using TagReader.FilesProviders;

namespace TagReaderUnitTests.Stubs
{
    public class FileProviderStub : IFilesProvider
    {
        public string File
        {
            get; 
            set;
        }

        public int FilesExistsCount
        {
            get; 
            set;
        }

        public bool DirectoryExists(string folderPath)
        {
            throw new NotImplementedException();
        }

        public bool FileExists(string filePath)
        {
            FilesExistsCount--;

            return (FilesExistsCount + 1) > 0;
        }

        public string[] GetDirectories(string folderPath)
        {
            throw new NotImplementedException();
        }

        public string[] GetFiles(string folderPath)
        {
            throw new NotImplementedException();
        }

        public void MoveFile(string sourceFilePath, string destinationFilePath)
        {
            File = destinationFilePath;
        }
    }
}
