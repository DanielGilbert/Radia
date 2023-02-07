using FluentAssertions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using Radia.Services;
using Radia.Services.FileProviders;
using Radia.Services.FileProviders.Local;
using Radia.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radia.Tests.ViewModels
{
    public class RadiaFileInfoViewModelTests
    {
        [TestClass]
        public class TheCtorMethod
        {
            public TestContext? TestContext { get; set; }

            [TestMethod]
            public void WhenGivenAllDependencies_ThenAllPropertiesAreSet()
            {
                string content = "FancyTestSite";
                string testFile = Path.Combine(TestContext!.TestRunResultsDirectory!, Guid.NewGuid().ToString().ToLowerInvariant().Replace('-', 'd'));

                File.WriteAllText(testFile, content);

                IByteSizeService byteSizeService = new ByteSizeService();

                IRadiaFileProvider radiaFileProvider = new LocalFileProvider(Path.GetDirectoryName(testFile), true);
                IRadiaFileInfo radiaFileInfo = radiaFileProvider.GetFileInfo(Path.GetFileName(testFile));

                RadiaFileInfoViewModel radiaFileInfoViewModel = new RadiaFileInfoViewModel(byteSizeService,
                                                                                           DefaultRadiaTestContext.WebHost,
                                                                                           radiaFileInfo,
                                                                                           false,
                                                                                           $"/{Path.GetFileName(testFile)}");

                radiaFileInfoViewModel.Name.Should().Be(Path.GetFileName(testFile));
                radiaFileInfoViewModel.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(10));
                radiaFileInfoViewModel.Length.Should().Be(13);
                radiaFileInfoViewModel.FormattedLength.Should().Be("13 bytes");
            }

            [TestMethod]
            public void WhenGivenAllDependenciesAndEmptyFile_ThenAllPropertiesAreSet()
            {
                string content = "";
                string testFile = Path.Combine(TestContext!.TestRunResultsDirectory!, Guid.NewGuid().ToString().ToLowerInvariant().Replace('-', 'd'));

                File.WriteAllText(testFile, content);

                IByteSizeService byteSizeService = new ByteSizeService();

                IRadiaFileProvider radiaFileProvider = new LocalFileProvider(Path.GetDirectoryName(testFile), true);
                IRadiaFileInfo radiaFileInfo = radiaFileProvider.GetFileInfo(Path.GetFileName(testFile));

                RadiaFileInfoViewModel radiaFileInfoViewModel = new RadiaFileInfoViewModel(byteSizeService,
                                                                                           DefaultRadiaTestContext.WebHost,
                                                                                           radiaFileInfo,
                                                                                           false,
                                                                                           $"/{Path.GetFileName(testFile)}");

                radiaFileInfoViewModel.Name.Should().Be(Path.GetFileName(testFile));
                radiaFileInfoViewModel.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(10));
                radiaFileInfoViewModel.Length.Should().Be(0);
                radiaFileInfoViewModel.FormattedLength.Should().Be("0 bytes");
            }

            [TestMethod]
            public void WhenGivenAllDependenciesAndDirectory_ThenAllPropertiesAreSet()
            {
                string content = "FancyTestSite";
                string directoryName = "NiceDirectory" + Path.DirectorySeparatorChar;
                string testDirectory = Path.Combine(TestContext!.TestRunResultsDirectory!, directoryName);

                Directory.CreateDirectory(testDirectory);

                IByteSizeService byteSizeService = new ByteSizeService();

                IRadiaFileProvider radiaFileProvider = new LocalFileProvider(TestContext!.TestRunResultsDirectory!, true);
                IRadiaFileInfo radiaFileInfo = radiaFileProvider.GetFileInfo(testDirectory);

                RadiaFileInfoViewModel radiaFileInfoViewModel = new RadiaFileInfoViewModel(byteSizeService,
                                                                                           DefaultRadiaTestContext.WebHost,
                                                                                           radiaFileInfo,
                                                                                           true,
                                                                                           $"/{testDirectory.Replace("\\","/")}");

                radiaFileInfoViewModel.Name.Should().Be(testDirectory);
                radiaFileInfoViewModel.Length.Should().Be(-1);
                radiaFileInfoViewModel.FormattedLength.Should().Be("-");
            }
        }
    }
}
