using System.IO;

using TagReader.Commands;
using TagReader.FilesProviders;
using TagReader.Logger;

namespace TagReader
{
    public class DirectoryFilesProcessor
    {
        #region Ctors
        
        public DirectoryFilesProcessor()
            : this(new LocalFilesProvider())
        { }

        public DirectoryFilesProcessor(IFilesProvider filesProvider)
        {
            FilesProvider = filesProvider;
        }

        #endregion

        #region Properties

        private IFilesProvider FilesProvider
        {
            get; 
            set;
        }

        public ILogger Logger
        {
            get;
            set;
        }

        public bool ProcessSubFolders
        {
            get;
            set;
        } 

        #endregion

        public void Run(string folderPath, ICommand command)
        {
            if (!FilesProvider.DirectoryExists(folderPath))
                return;

            var name = Path.GetDirectoryName(folderPath);
            if (string.IsNullOrEmpty(name))
                name = folderPath;

            Log(string.Format("Start processing '{0}' folder.", name));

            if (ProcessSubFolders)
                RunSubFolders(folderPath, command);

            LoggerIndent();

            var files = FilesProvider.GetFiles(folderPath);

            foreach (var filePath in files)
            {
                command.Execute(folderPath, filePath, Logger);
            }
            
            LoggerUnIndent();

            Log(string.Format("End processing '{0}' folder.", name));
        }

        private void RunSubFolders(string parentFolderPath, ICommand command)
        {
            var subFolders = FilesProvider.GetDirectories(parentFolderPath);

            foreach (var subFolder in subFolders)
            {
                LoggerIndent();
                Run(subFolder, command);
                LoggerUnIndent();
            }
        }

        private void LoggerIndent()
        {
            if (Logger != null)
                Logger.Indent();
        }

        private void LoggerUnIndent()
        {
            if (Logger != null)
                Logger.UnIndent();
        }

        private void Log(string message)
        {
            if (Logger != null)
                Logger.WriteLine(message);
        }
    }
}