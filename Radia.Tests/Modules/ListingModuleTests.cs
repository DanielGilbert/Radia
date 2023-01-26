using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Moq;
using Radia.Factories;
using Radia.Modules;
using Radia.Services;
using Radia.Services.FileProviders;
using Radia.Services.FileProviders.Local;

namespace Radia.Tests.Modules
{
    public class ListingModuleTests
    {
        public class RadiaTestContext
        {
            public string WebRootPath = @"./webroot/path/";
            public string SubFolderPath = @"/test";
            public IWebHostEnvironment WebHostEnvironment { get; }
            public IRadiaFileProviderFactory FileProviderFactory { get; }
            public IRadiaFileProvider LocalFileProvider { get; }
            public IRadiaFileProvider GitFileProvider { get; }
            public IConfigurationService ConfigurationService { get; }
            public IContentTypeIdentifierService ContentTypeIdentifierService { get; }
            public IViewModelFactory ViewModelFactory { get; }
            public IContentProcessorFactory<string> ContentProcessorFactory { get; }
            public IHttpContextAccessor HttpContextAccessor { get; }
            public IViewFactory ViewFactory { get; set; }

            public RadiaTestContext()
            {
                HttpContextAccessor = MockHttpContextAccessor();
                ContentProcessorFactory = BuildContentProcessorFactory();
                WebHostEnvironment = MockWebHostEnvironment();
                LocalFileProvider = MockLocalFileProvider();
                GitFileProvider = MockGitFileProvider();
                ConfigurationService = MockConfigurationService();
                ContentTypeIdentifierService = BuildContentTypeIdentifierService();
                FileProviderFactory = BuildFileProviderFactory();
                ViewFactory = BuildViewFactory();
                ViewModelFactory = new ViewModelFactory(FileProviderFactory,
                                                        ConfigurationService,
                                                        ContentTypeIdentifierService,
                                                        ContentProcessorFactory,
                                                        HttpContextAccessor);
            }

            private static IViewFactory BuildViewFactory()
            {
                return new ViewFactory();
            }

            private static IHttpContextAccessor MockHttpContextAccessor()
            {
                var httpContextAccessor = new Mock<IHttpContextAccessor>();
                httpContextAccessor.Setup(p => p.HttpContext!.Request.Host).Returns(new HostString("localhost:54321"));
                httpContextAccessor.Setup(p => p.HttpContext!.Request.Scheme).Returns("http");
                return httpContextAccessor.Object;
            }

            private static IContentProcessorFactory<string> BuildContentProcessorFactory()
            {
                return new ContentProcessorFactory();
            }

            private static IContentTypeIdentifierService BuildContentTypeIdentifierService()
            {
                return new ContentTypeIdentifierService();
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

            private static IRadiaFileProvider MockGitFileProvider()
            {
                var localFileProvider = new Mock<IRadiaFileProvider>();
                localFileProvider.Setup(p => p.FileProviderEnum).Returns(FileProviderEnum.Local);
                return localFileProvider.Object;
            }

            private static IRadiaFileProvider MockLocalFileProvider()
            {
                var localFileProvider = new Mock<IRadiaFileProvider>();
                localFileProvider.Setup(p => p.FileProviderEnum).Returns(FileProviderEnum.Git);
                return localFileProvider.Object;
            }

            private IRadiaFileProviderFactory BuildFileProviderFactory()
            {
                var fileProviders = new List<IRadiaFileProvider>
                {
                    GitFileProvider,
                    LocalFileProvider
                };
                var factory = new FileProviderFactory(fileProviders);

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
                                            RadiaTestContext.ViewFactory,
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
                                            RadiaTestContext.ViewFactory,
                                            RadiaTestContext.ConfigurationService);

                var result = sut.ProcessRequest(RadiaTestContext.SubFolderPath);

                result.Should().NotBeNull();
            }
        }
    }
}
