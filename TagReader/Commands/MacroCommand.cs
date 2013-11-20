using System.Collections.Generic;

using TagReader.Logger;

namespace TagReader.Commands
{
    public class MacroCommand : ICommand
    {
        public List<ICommand> Commands
        {
            get; 
            set;
        }

        public void Execute(string folderPath, string filePath, ILogger logger)
        {
            if (Commands != null)
            {
                foreach (var command in Commands)
                {
                    command.Execute(folderPath, filePath, logger);
                }
            }
        }
    }
}