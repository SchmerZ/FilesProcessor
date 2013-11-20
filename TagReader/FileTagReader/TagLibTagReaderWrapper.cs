using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace TagReader.FileTagReader
{
    internal class TagLibTagReaderWrapper : IFileTagReader
    {
        public FileTag Read(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException();

            var file = TagLib.File.Create(filePath);

            var result = new FileTag
                {
                    FilePath = filePath,
                    JoinedAlbumnArtists = ToUtf8(file.Tag.JoinedAlbumArtists),
                    JoinedPerformers = ToUtf8(file.Tag.JoinedPerformers),
                    Title = ToUtf8(file.Tag.Title)
                };

            return result;
        }

        private static string ToUtf8(string unknown)
        {
            if (string.IsNullOrEmpty(unknown))
                return string.Empty;

            return new string(unknown.ToCharArray().
                Select(x => ((x + 848) >= 'А' && (x + 848) <= 'ё') ? (char)(x + 848) : x).
                ToArray());
        }

        public bool IsFileFormatSupported(string filePath)
        {
            var mimetype = GetMimeType(filePath);

            return TagLib.FileTypes.AvailableTypes.ContainsKey(mimetype);
        }

        private string GetMimeType(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("filePath");

            var ext = string.Empty;

            var index = filePath.LastIndexOf(".") + 1;

            if (index >= 1 && index < filePath.Length)
                ext = filePath.Substring(index, filePath.Length - index);

            var mimetype = "taglib/" + ext.ToLower(CultureInfo.InvariantCulture);

            return mimetype;
        }

        public void SaveTag(FileTag tag)
        {
            var file = TagLib.File.Create(tag.FilePath);

            file.Tag.Title = tag.Title;
            file.Tag.AlbumArtists = new [] {tag.JoinedAlbumnArtists};

            file.Save();
        }
    }
}