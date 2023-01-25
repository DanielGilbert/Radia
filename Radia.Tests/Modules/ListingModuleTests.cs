using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Moq;
using Radia.Factories;
using Radia.Modules;
using Radia.Services;

namespace Radia.Tests.Modules
{
    public class ListingModuleTests
    {
        public class RadiaTestContext
        {
            public string WebRootPath = @"./webroot/path/";
            public string SubFolderPath = @"/test";
            public IWebHostEnvironment WebHostEnvironment { get; }
            public IFileProviderFactory FileProviderFactory { get; }
            public IFileProvider LocalFileProvider { get; }
            public IFileProvider GitFileProvider { get; }
            public IConfigurationService ConfigurationService { get; }
            public IViewModelFactory ViewModelFactory { get; }

            public RadiaTestContext()
            {
                WebHostEnvironment = MockWebHostEnvironment();
                LocalFileProvider = MockLocalFileProvider();
                GitFileProvider = MockGitFileProvider();
                ConfigurationService = MockConfigurationService();
                FileProviderFactory = BuildFileProviderFactory();
                ViewModelFactory = new ViewModelFactory();
            }

            private static IConfigurationService MockConfigurationService()
            {
                var myConfiguration = CreateValidConfiguration();

                var configuration = new ConfigurationBuilder()
                    .AddInMemoryCollection(myConfiguration)
                    .Build();

                var configurationService = new ConfigurationService(configuration);

                return configurationService;
            }

            private static Dictionary<string, string?> CreateValidConfiguration()
            {
                var result = new Dictionary<string, string?>
                {
                    {"AllowedHosts", "*"},
                    {"AppConfiguration:FileProviderConfiguration:FileProvider", "Local"},
                    {"AppConfiguration:FileProviderConfiguration:Settings:RootDirectory", "/blogsource/"},
                    {"AppConfiguration:WebsiteTitle", "g5t.de - 'cause gilbert.de was too expensive..."},
                    {"AppConfiguration:DefaultPageHeader", "g[ilber]t.de" }
                };

                return result;
            }

            private static IFileProvider MockGitFileProvider()
            {
                var localFileProvider = new Mock<IFileProvider>();

                return localFileProvider.Object;
            }

            private static IFileProvider MockLocalFileProvider()
            {
                var localFileProvider = new Mock<IFileProvider>();
                
                return localFileProvider.Object;
            }

            private IFileProviderFactory BuildFileProviderFactory()
            {
                var factory = new FileProviderFactory(GitFileProvider, LocalFileProvider);

                return factory;
            }

            private IWebHostEnvironment MockWebHostEnvironment()
            {
                var webHostEnvironment = new Mock<IWebHostEnvironment>();

                webHostEnvironment.Setup((env) => env.WebRootPath).Returns(WebRootPath);

                return webHostEnvironment.Object;
            }
        }

        [TestClass]
        public class TheProcessRequestMethod
        {
            RadiaTestContext RadiaTestContext { get; set; }

            public TheProcessRequestMethod()
            {
                RadiaTestContext = new RadiaTestContext();
            }

            [TestInitialize]
            public void InitializeTest()
            {
                RadiaTestContext = new RadiaTestContext();
            }

            [TestMethod]
            public void WhenCalled_ThenWillReturnTheFolderViewModel()
            {
                var sut = new ListingModule(RadiaTestContext.WebHostEnvironment,
                                            RadiaTestContext.FileProviderFactory,
                                            RadiaTestContext.ViewModelFactory,
                                            RadiaTestContext.ConfigurationService);

                var result = sut.ProcessRequest();
                
                result.Should().NotBeNull();
            }
        }

        [TestClass]
        public class TheProcessRequestWithArgsMethod
        {
            RadiaTestContext RadiaTestContext { get; }

            public TheProcessRequestWithArgsMethod()
            {
                RadiaTestContext = new RadiaTestContext();
            }

            [TestMethod]
            public void WhenCalled_ThenWillReturnTheFolderViewModelAndWillBeInTheGivenFolder()
            {
                var sut = new ListingModule(RadiaTestContext.WebHostEnvironment,
                                            RadiaTestContext.FileProviderFactory,
                                            RadiaTestContext.ViewModelFactory,
                                            RadiaTestContext.ConfigurationService);

                var result = sut.ProcessRequest(RadiaTestContext.SubFolderPath);

                result.Should().NotBeNull();
            }
        }
    }
}
