using FluentAssertions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using Moq;
using Radia.Exceptions;
using Radia.Services.FileProviders;
using Radia.Services.FileProviders.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radia.Tests.Services.FileProviders.Local
{
    public class LocalFileProviderTests
    {
        [TestClass]
        public class TheCtorMethod
        {
            public TestContext? TestContext { get; set; }

            [TestMethod]
            public void GivenALocalFileProviderConfiguration_ThenTheReturnedFileProviderWillSignalLocal()
            {
                var fileProviderMock = new Mock<IFileProvider>();
                var defaultRadiaTestContext = new DefaultRadiaTestContext(FileProviderEnum.Local, TestContext.TestRunDirectory);

                var sut = new LocalFileProvider(defaultRadiaTestContext.ValidFileProviderConfigurations, fileProviderMock.Object);

                sut.FileProviderEnum.Should().Be(FileProviderEnum.Local);
            }

            [TestMethod]
            public void GivenALocalFileProviderConfiguration_ThenPhysicalFileProviderWillBeCalled()
            {
                var defaultRadiaTestContext = new DefaultRadiaTestContext(FileProviderEnum.Local, TestContext.TestRunDirectory);
                var configuration = defaultRadiaTestContext.ValidFileProviderConfigurations;
                configuration.Settings["RootDirectory"] = TestContext?.DeploymentDirectory ?? string.Empty;

                var sut = new LocalFileProvider(configuration);

                sut.FileProviderEnum.Should().Be(FileProviderEnum.Local);
            }
        }

        [TestClass]
        public class TheGetDirectoryContentMethod
        {
            public TestContext? TestContext { get; set; }

            [TestMethod]
            public void WhenCalled_ThenAnInstanceOfIDirectoryInfoWillBeReturned()
            {
                var fileProviderMock = new Mock<IFileProvider>();
                fileProviderMock.Setup(x => x.GetDirectoryContents(It.IsAny<string>())).Returns(new Mock<IDirectoryContents>().Object);

                var defaultRadiaTestContext = new DefaultRadiaTestContext(FileProviderEnum.Local, TestContext.TestRunDirectory);

                var sut = new LocalFileProvider(defaultRadiaTestContext.ValidFileProviderConfigurations, fileProviderMock.Object);
                var result = sut.GetDirectoryContents(TestContext?.DeploymentDirectory ?? string.Empty);
                sut.FileProviderEnum.Should().Be(FileProviderEnum.Local);
                fileProviderMock.Verify(x => x.GetDirectoryContents(It.IsAny<string>()));
            }
        }

        [TestClass]
        public class TheGetFileInfoMethod
        {
            public TestContext? TestContext { get; set; }

            [TestMethod]
            public void WhenCalled_ThenAnInstanceOfIFileInfoWillBeReturned()
            {
                var fileProviderMock = new Mock<IFileProvider>();
                fileProviderMock.Setup(x => x.GetFileInfo(It.IsAny<string>())).Returns(new Mock<IFileInfo>().Object);

                var defaultRadiaTestContext = new DefaultRadiaTestContext(FileProviderEnum.Local, TestContext.TestRunDirectory);

                var sut = new LocalFileProvider(defaultRadiaTestContext.ValidFileProviderConfigurations, fileProviderMock.Object);
                var result = sut.GetFileInfo(TestContext?.DeploymentDirectory ?? string.Empty);
                sut.FileProviderEnum.Should().Be(FileProviderEnum.Local);
                fileProviderMock.Verify(x => x.GetFileInfo(It.IsAny<string>()));
            }
        }

        [TestClass]
        public class TheWatchMethod
        {
            public TestContext? TestContext { get; set; }

            [TestMethod]
            public void WhenCalled_ThenAnInstanceOfIChangeTokenWillBeReturned()
            {
                var fileProviderMock = new Mock<IFileProvider>();
                fileProviderMock.Setup(x => x.Watch(It.IsAny<string>())).Returns(new Mock<IChangeToken>().Object);

                var defaultRadiaTestContext = new DefaultRadiaTestContext(FileProviderEnum.Local, TestContext.TestRunDirectory);

                var sut = new LocalFileProvider(defaultRadiaTestContext.ValidFileProviderConfigurations, fileProviderMock.Object);
                var result = sut.Watch(TestContext?.DeploymentDirectory ?? string.Empty);
                sut.FileProviderEnum.Should().Be(FileProviderEnum.Local);
                fileProviderMock.Verify(x => x.Watch(It.IsAny<string>()));
            }
        }
    }
}
