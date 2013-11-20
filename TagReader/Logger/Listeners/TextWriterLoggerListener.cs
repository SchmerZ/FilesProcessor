using System;
using System.IO;

namespace TagReader.Logger.Listeners
{
    public class TextWriterLoggerListener : LoggerListener
    {
        public TextWriterLoggerListener(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            
            Writer = new StreamWriter(stream);
        }

        public TextWriterLoggerListener(TextWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            Writer = writer;
        }

        protected TextWriter Writer
        {
            get;
            set;
        }

        public override void WriteLine(string message)
        {
            if (NeedIndent)
                WriteIndent();

            try
            {
                Writer.WriteLine(message);
                NeedIndent = true;
            }
            catch (ObjectDisposedException)
            {
            }
        }

        public override void Write(string message)
        {
            if (NeedIndent)
                WriteIndent();

            try
            {
                Writer.Write(message);
            }
            catch (ObjectDisposedException)
            {
            }
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    Writer.Close();
                }
                else
                {
                    if (Writer != null)
                    {
                        try
                        {
                            Writer.Close();
                        }
                        catch (ObjectDisposedException)
                        {
                        }
                    }
                    Writer = null;
                }
            }
            finally
            {
                
            }
        }
    }
}