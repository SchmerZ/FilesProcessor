using TagReader.Logger;

namespace TagReader.Commands
{
    public interface ICommand
    {
        void Execute(string folderPath, string filePath, ILogger logger);
    }
}