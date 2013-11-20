using System;
using System.Collections.Generic;
using System.Linq;

using TagReader.Commands;
using TagReader.Logger;
using TagReader.Logger.Listeners;

namespace ID3TagReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = GetArgValue(args, "\\t");
            if (string.IsNullOrEmpty(path))
                return;

            var logger = GetLogger(args);

            var processSubFolders = false;
            var processSubFoldersValue = GetArgValue(args, "\\subfolders");

            if (!string.IsNullOrEmpty(processSubFoldersValue))
                processSubFolders = Convert.ToBoolean(processSubFoldersValue);

            var processor = new TagReader.DirectoryFilesProcessor
                {
                    Logger = logger,
                    ProcessSubFolders = processSubFolders
                };

            var command = new MacroCommand
                {
                    Commands = new List<ICommand>
                        {
                            new FileRenamerByTagCommand(),
                            new FileTageSetterByNameCommand()
                        }
                };

            processor.Run(path, command);
        }

        private static ILogger GetLogger(string[] args)
        {
            if (args.Length == 0)
                return null;

            var logger = new Logger();

            if (args.Any(o => o == "\\log"))
                logger.Listeners.Add(new ConsoleLoggerListener());

            var outputLogFile = GetArgValue(args, "\\output");

            if (!string.IsNullOrEmpty(outputLogFile))
            {
            }

            return logger;
        }

        private static string GetArgValue(string[] args, string key)
        {
            var arg = args.FirstOrDefault(o => o.StartsWith(key));

            if (arg == null)
                return null;

            var index = arg.IndexOf(':');
            var value = arg.Substring(index + 1);

            return value;
        }
    }
}