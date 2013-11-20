using System.IO;
using TagReader.FileTagReader;
using TagReader.FilesProviders;
using TagReader.Logger;

namespace TagReader.Commands
{
    public class FileRenamerByTagCommand : FileTagCommand
    {
        public FileRenamerByTagCommand()
            : this(new LocalFilesProvider(), TagReaderFactory.GetFileTagReader())
        { }

        public FileRenamerByTagCommand(IFilesProvider filesProvider, IFileTagReader fileTagReader)
            : base (fileTagReader)
        {
            FilesProvider = filesProvider;
        }

        private IFilesProvider FilesProvider
        {
            get; 
            set;
        }

        protected override void ExecuteCommand(string folderPath, string filePath, ILogger logger)
        {
            var fileName = Path.GetFileName(filePath);

            if (!string.IsNullOrEmpty(fileName) && fileName.Contains(" - "))
            {
                if (logger != null)
                    logger.WriteLine("SKIPPED :: File already has a nice naming.");

                return;
            }

            var tag = FileTagReader.Read(filePath);

            if (tag == null ||
                (string.IsNullOrEmpty(tag.JoinedAlbumnArtists) && 
                string.IsNullOrEmpty(tag.JoinedPerformers)) || 
                string.IsNullOrEmpty(tag.Title))
            {
                if (logger != null)
                    logger.WriteLine("SKIPPED :: File doesn't has tag info.");

                return;
            }
            
            var newFileNamePath = GenerateFileNameByTag(folderPath, filePath, tag);

            FilesProvider.MoveFile(filePath, newFileNamePath);

            if (logger != null)
            {
                var newFileName = Path.GetFileName(newFileNamePath);
                logger.WriteLine(string.Format("DONE :: New file name is '{0}'.", newFileName));
            }
        }

        private string GenerateFileNameByTag(string folderPath, string filePath, FileTag tag, int count = 0)
        {
            var extension = Path.GetExtension(filePath);
            var artist = string.IsNullOrEmpty(tag.JoinedAlbumnArtists)
                             ? tag.JoinedPerformers
                             : tag.JoinedAlbumnArtists;

            var newFileName = string.Format("{0} - {1}", artist, tag.Title);

            if (count != 0)
                newFileName += string.Format(" ({0})", count);

            newFileName = newFileName + extension;

            var result = Path.Combine(folderPath, newFileName);

            if (FilesProvider.FileExists(result))
                return GenerateFileNameByTag(folderPath, filePath, tag, ++count);

            return result;
        }
    }
}