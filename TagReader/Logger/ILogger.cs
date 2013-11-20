namespace TagReader.Logger
{
    public interface ILogger
    {
        void Write(string message);
        void WriteLine(string message);

        void Indent();
        void UnIndent();
    }
}