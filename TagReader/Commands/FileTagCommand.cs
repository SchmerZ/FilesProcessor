using System.IO;

using TagReader.FileTagReader;
using TagReader.Logger;

namespace TagReader.Commands
{
    public abstract class FileTagCommand : ICommand
    {
        protected FileTagCommand()
            : this(TagReaderFactory.GetFileTagReader())
        { }

        protected FileTagCommand(IFileTagReader fileTagReader)
        {
            FileTagReader = fileTagReader;
        }

        public IFileTagReader FileTagReader
        {
            get;
            set;
        }

        public void Execute(string folderPath, string filePath, ILogger logger)
        {
            var needToExecute = true;

            var fileName = Path.GetFileName(filePath);

            if (logger != null)
            {
                logger.WriteLine(string.Format("Start processing '{0}' file.", fileName));
                logger.Indent();
            }

            if (!FileTagReader.IsFileFormatSupported(filePath))
            {
                needToExecute = false;

                if (logger != null)
                    logger.WriteLine("SKIPPED :: File is not supported.");
            }

            if (needToExecute)
                ExecuteCommand(folderPath, filePath, logger);

            if (logger != null)
            {
                logger.UnIndent();

                logger.WriteLine(string.Format("End processing '{0}'.", fileName));
                logger.WriteLine(string.Empty);
            }
        }

        protected abstract void ExecuteCommand(string folderPath, string filePath, ILogger logger);
    }
}