using System;

namespace TagReader.Logger.Listeners
{
    public abstract class LoggerListener : IDisposable
    {
        protected LoggerListener()
        {
            IndentLevel = 0;
            NeedIndent = true;
        }

        protected bool NeedIndent
        {
            get; 
            set;
        }

        public int IndentLevel
        {
            get; 
            set;
        }

        public abstract void WriteLine(string message);

        public abstract void Write(string message);

        protected virtual void WriteIndent()
        {
            NeedIndent = false;

            for (var i = 0; i < IndentLevel; i++)
            {
                Write("    ");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}