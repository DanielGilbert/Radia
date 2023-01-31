using FluentAssertions;
using Moq;
using Radia.Factories;
using Radia.Factories.View;
using Radia.Services.ContentProcessors;
using Radia.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radia.Tests.Factories
{
    public class ViewFactoryTests
    {
        [TestClass]
        public class TheCreateMethod
        {
            public TestContext? TestContext { get; set; }

            [TestMethod]
            public void WhenGivenAFolderViewModel_WillReturnFolderView()
            {
                DefaultRadiaTestContext defaultRadiaTestContext = new(TestContext!.TestRunDirectory!);
                FolderViewModel folderViewModel = new FolderViewModel(string.Empty,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      defaultRadiaTestContext.FooterService);
                ViewFactory viewFactory = new();

                string result = viewFactory.Create(folderViewModel);

                result.Should().Be("FolderView");
            }

            [TestMethod]
            public void WhenGivenAPathNotFoundViewModel_WillReturnPathNotFoundView()
            {
                DefaultRadiaTestContext defaultRadiaTestContext = new(TestContext!.TestRunDirectory!);
                PathNotFoundViewModel viewModel = new PathNotFoundViewModel(string.Empty,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      defaultRadiaTestContext.FooterService);
                ViewFactory viewFactory = new();

                string result = viewFactory.Create(viewModel);

                result.Should().Be("PathNotFoundView");
            }

            [TestMethod]
            public void WhenGivenADownloadableFileViewModel_WillReturnPhysicalFileView()
            {
                DefaultRadiaTestContext defaultRadiaTestContext = new(TestContext!.TestRunDirectory!);
                DownloadableFileViewModel viewModel = new DownloadableFileViewModel(null,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      defaultRadiaTestContext.FooterService);
                ViewFactory viewFactory = new();

                string result = viewFactory.Create(viewModel);

                result.Should().Be("PhysicalFileView");
            }

            [TestMethod]
            public void WhenGivenAProcessedFileViewModel_WillReturnProcessedFileView()
            {
                DefaultRadiaTestContext defaultRadiaTestContext = new(TestContext!.TestRunDirectory!);
                ProcessedFileViewModel viewModel = new ProcessedFileViewModel(string.Empty,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      string.Empty,
                                                                      defaultRadiaTestContext.FooterService);
                ViewFactory viewFactory = new();

                string result = viewFactory.Create(viewModel);

                result.Should().Be("ProcessedFileView");
            }

            [TestMethod]
            public void WhenGivenAnUnkownViewModel_WillReturnAnEmptyString()
            {
                UnknownViewModel viewModel = new UnknownViewModel();

                ViewFactory viewFactory = new();

                string result = viewFactory.Create(viewModel);

                result.Should().BeEmpty();
            }

            public class UnknownViewModel : IViewModel
            {
                public string PageTitle => throw new NotImplementedException();

                public string PageHeader => throw new NotImplementedException();

                public string WebsiteRoot => throw new NotImplementedException();

                public string FooterContent => throw new NotImplementedException();
            }
        }
    }
}
