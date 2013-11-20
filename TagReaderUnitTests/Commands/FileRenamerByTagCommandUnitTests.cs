using Microsoft.VisualStudio.TestTools.UnitTesting;

using TagReader;
using TagReader.Commands;
using TagReaderUnitTests.Stubs;

namespace TagReaderUnitTests.Commands
{
    [TestClass]
    public class FileRenamerByTagCommandUnitTests
    {
        [TestMethod]
        public void FileRenamerByTagCommand_FileFormatNotSpoorted_Skipped()
        {
            var fileName = "file.mp3";

            var fileProviderStub = new FileProviderStub
                {
                    File = fileName
                };

            var tagReaderStub = new FileTagReaderStub();

            var command = new FileRenamerByTagCommand(fileProviderStub, tagReaderStub);

            command.Execute("folder_A", fileName, null);

            Assert.AreEqual(fileName, fileProviderStub.File);
        }

        [TestMethod]
        public void FileRenamerByTagCommand_NullFileTag_Skipped()
        {
            var fileName = "file.mp3";

            var fileProviderStub = new FileProviderStub
            {
                File = fileName
            };

            var tagReaderStub = new FileTagReaderStub
                {
                    IsFileSupported = true
                };

            var command = new FileRenamerByTagCommand(fileProviderStub, tagReaderStub);

            command.Execute("folder_A", fileName, null);

            Assert.AreEqual(fileName, fileProviderStub.File);
        }

        [TestMethod]
        public void FileRenamerByTagCommand_WithTag_Renamed()
        {
            var fileName = "file.mp3";

            var fileProviderStub = new FileProviderStub
            {
                File = fileName
            };

            var tagReaderStub = new FileTagReaderStub
            {
                IsFileSupported = true,
                Tag = new FileTag
                    {
                        Title = "title",
                        JoinedAlbumnArtists = "artists"
                    }
            };

            var command = new FileRenamerByTagCommand(fileProviderStub, tagReaderStub);

            command.Execute("folder_A", fileName, null);

            Assert.AreEqual("folder_A\\artists - title.mp3", fileProviderStub.File);
        }

        [TestMethod]
        public void FileRenamerByTagCommand_FileAlreadtExists_IncrementedName()
        {
            var fileName = "file.mp3";

            var fileProviderStub = new FileProviderStub
            {
                File = fileName,
                FilesExistsCount = 1
            };

            var tagReaderStub = new FileTagReaderStub
            {
                IsFileSupported = true,
                Tag = new FileTag
                {
                    Title = "title",
                    JoinedAlbumnArtists = "artists"
                }
            };

            var command = new FileRenamerByTagCommand(fileProviderStub, tagReaderStub);

            command.Execute("folder_A", fileName, null);

            Assert.AreEqual("folder_A\\artists - title (1).mp3", fileProviderStub.File);
        }
    }
}