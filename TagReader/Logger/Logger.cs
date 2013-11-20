using System.Collections.Generic;

using TagReader.Logger.Listeners;

namespace TagReader.Logger
{
    public class Logger : ILogger
    {
        public Logger()
        {
            Listeners = new List<LoggerListener>();
        }

        public List<LoggerListener> Listeners
        {
            get; 
            private set;
        }

        private int IndentLevel
        {
            get; 
            set;
        }

        public void Write(string message)
        {
            foreach (var listener in Listeners)
            {
                listener.Write(message);
            }
        }

        public void WriteLine(string message)
        {
            foreach (var listener in Listeners)
            {
                listener.WriteLine(message);
            }
        }

        public void Indent()
        {
            IndentLevel++;

            foreach (var listener in Listeners)
            {
                listener.IndentLevel = IndentLevel;
            }
        }

        public void UnIndent()
        {
            IndentLevel--;

            foreach (var listener in Listeners)
            {
                listener.IndentLevel = IndentLevel;
            }
        }
    }
}