using System;

namespace TagReader.Logger.Listeners
{
    public class ConsoleLoggerListener : TextWriterLoggerListener
    {
        public ConsoleLoggerListener() : 
            base(Console.Out)
        { }
    }
}