using System;
using System.IO;

using TagReader.Logger;

namespace TagReader.Commands
{
    public class FileTageSetterByNameCommand : FileTagCommand
    {
        protected override void ExecuteCommand(string folderPath, string filePath, ILogger logger)
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);

            if (!fileName.Contains(" - "))
            {
                if (logger != null)
                    logger.WriteLine("SKIPPED :: File doesn't has a nice naming.");

                return;
            }

            var data = fileName.Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries);

            if (data.Length != 2)
            {
                if (logger != null)
                    logger.WriteLine("SKIPPED :: File doesn't has a correct name.");

                return;
            }

            var artist = data[0];
            var title = data[1];

            var tag = FileTagReader.Read(filePath);

            if (!string.IsNullOrEmpty(tag.JoinedAlbumnArtists) || !string.IsNullOrEmpty(tag.Title))
            {
                if (logger != null)
                    logger.WriteLine("SKIPPED :: File already has tag info.");

                return;
            }

            tag.JoinedAlbumnArtists = artist;
            tag.Title = title;

            FileTagReader.SaveTag(tag);
        }
    }
}