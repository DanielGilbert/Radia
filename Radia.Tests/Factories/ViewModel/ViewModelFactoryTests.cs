using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using Moq;
using Radia.Factories.ViewModel;
using Radia.Services;
using Radia.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Radia.Tests.Factories.ViewModel
{
    public class ViewModelFactoryTests
    {
        [TestClass]
        public class TheCreateMethod
        {
            public TestContext? TestContext { get; set; }

            [TestCategory("IntegrationTest")]
            [TestMethod]
            public void WhenGivenAFolderPath_WillReturnTheFolderViewModel()
            {
                DefaultRadiaTestContext defaultRadiaTestContext = new DefaultRadiaTestContext(TestContext!.TestRunResultsDirectory!);

                var logger = Mock.Of<ILogger<ViewModelFactory>>();

                ViewModelFactory viewModelFactory = new ViewModelFactory(defaultRadiaTestContext.FileProvider,
                                                                         defaultRadiaTestContext.ConfigurationService,
                                                                         defaultRadiaTestContext.ContentTypeIdentifierService,
                                                                         defaultRadiaTestContext.ContentProcessorFactory,
                                                                         defaultRadiaTestContext.HttpContextAccessor,
                                                                         new ByteSizeService(),
                                                                         defaultRadiaTestContext.FooterService,
                                                                         logger);

                ViewModelFactoryArgs factoryArgs = new ViewModelFactoryArgs("/", "TestTitle");

                IViewModel result = viewModelFactory.Create(factoryArgs);

                result.Should().BeOfType<FolderViewModel>();
            }

            [TestCategory("IntegrationTest")]
            [TestMethod]
            public void WhenGivenAnInvalidFolderPath_WillReturnTheFileNotFoundViewModel()
            {
                DefaultRadiaTestContext defaultRadiaTestContext = new DefaultRadiaTestContext(TestContext!.TestRunResultsDirectory!);

                var logger = Mock.Of<ILogger<ViewModelFactory>>();

                ViewModelFactory viewModelFactory = new ViewModelFactory(defaultRadiaTestContext.FileProvider,
                                                                         defaultRadiaTestContext.ConfigurationService,
                                                                         defaultRadiaTestContext.ContentTypeIdentifierService,
                                                                         defaultRadiaTestContext.ContentProcessorFactory,
                                                                         defaultRadiaTestContext.HttpContextAccessor,
                                                                         new ByteSizeService(),
                                                                         defaultRadiaTestContext.FooterService,
                                                                         logger);

                ViewModelFactoryArgs factoryArgs = new ViewModelFactoryArgs("/invalidViewModel", "TestTitle");

                IViewModel result = viewModelFactory.Create(factoryArgs);

                result.Should().BeOfType<PathNotFoundViewModel>();
            }

            [TestCategory("IntegrationTest")]
            [TestMethod]
            public void WhenGivenAValidFilePath_WillReturnTheDownloadableFileViewModel()
            {
                DefaultRadiaTestContext defaultRadiaTestContext = new DefaultRadiaTestContext(TestContext!.TestRunResultsDirectory!);

                string path = Path.Combine(TestContext!.TestRunResultsDirectory!, "TestFolder1" + Path.DirectorySeparatorChar + "test.txt");

                File.WriteAllText(path, "test");

                var logger = Mock.Of<ILogger<ViewModelFactory>>();

                ViewModelFactory viewModelFactory = new ViewModelFactory(defaultRadiaTestContext.FileProvider,
                                                                         defaultRadiaTestContext.ConfigurationService,
                                                                         defaultRadiaTestContext.ContentTypeIdentifierService,
                                                                         defaultRadiaTestContext.ContentProcessorFactory,
                                                                         defaultRadiaTestContext.HttpContextAccessor,
                                                                         new ByteSizeService(),
                                                                         defaultRadiaTestContext.FooterService,
                                                                         logger);

                ViewModelFactoryArgs factoryArgs = new ViewModelFactoryArgs("/test.txt", "TestTitle");

                IViewModel result = viewModelFactory.Create(factoryArgs);

                result.Should().BeOfType<DownloadableFileViewModel>();
            }

            [TestCategory("IntegrationTest")]
            [TestMethod]
            public void WhenGivenAValidLargeFilePath_WillReturnTheDownloadableFileViewModel()
            {
                DefaultRadiaTestContext defaultRadiaTestContext = new DefaultRadiaTestContext(TestContext!.TestRunResultsDirectory!);

                string path = Path.Combine(TestContext!.TestRunResultsDirectory!, "TestFolder1" + Path.DirectorySeparatorChar + "test2.png");

                byte[] data = new byte[1024 * 1024 * 4];
                File.WriteAllBytes(path, data);

                var logger = Mock.Of<ILogger<ViewModelFactory>>();

                ViewModelFactory viewModelFactory = new ViewModelFactory(defaultRadiaTestContext.FileProvider,
                                                                         defaultRadiaTestContext.ConfigurationService,
                                                                         defaultRadiaTestContext.ContentTypeIdentifierService,
                                                                         defaultRadiaTestContext.ContentProcessorFactory,
                                                                         defaultRadiaTestContext.HttpContextAccessor,
                                                                         new ByteSizeService(),
                                                                         defaultRadiaTestContext.FooterService,
                                                                         logger);

                ViewModelFactoryArgs factoryArgs = new ViewModelFactoryArgs("/test2.png", "TestTitle");

                IViewModel result = viewModelFactory.Create(factoryArgs);

                result.Should().BeOfType<DownloadableFileViewModel>();
            }

            [TestCategory("IntegrationTest")]
            [TestMethod]
            public void WhenGivenAMarkdownFilePath_WillReturnTheProcessedFileViewModel()
            {
                DefaultRadiaTestContext defaultRadiaTestContext = new DefaultRadiaTestContext(TestContext!.TestRunResultsDirectory!);

                string path = Path.Combine(TestContext!.TestRunResultsDirectory!, "TestFolder1" + Path.DirectorySeparatorChar + "test.md");

                File.WriteAllText(path, "## Header2");

                var logger = Mock.Of<ILogger<ViewModelFactory>>();

                ViewModelFactory viewModelFactory = new ViewModelFactory(defaultRadiaTestContext.FileProvider,
                                                                         defaultRadiaTestContext.ConfigurationService,
                                                                         defaultRadiaTestContext.ContentTypeIdentifierService,
                                                                         defaultRadiaTestContext.ContentProcessorFactory,
                                                                         defaultRadiaTestContext.HttpContextAccessor,
                                                                         new ByteSizeService(),
                                                                         defaultRadiaTestContext.FooterService,
                                                                         logger);

                ViewModelFactoryArgs factoryArgs = new ViewModelFactoryArgs("/test.md", "TestTitle");

                IViewModel result = viewModelFactory.Create(factoryArgs);

                result.Should().BeOfType<ProcessedFileViewModel>();
            }
        }
    }
}
