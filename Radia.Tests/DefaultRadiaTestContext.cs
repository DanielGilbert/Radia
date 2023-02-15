using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using Moq;
using Radia.Factories;
using Radia.Factories.ContentProcessor;
using Radia.Factories.FileProvider;
using Radia.Factories.View;
using Radia.Factories.ViewModel;
using Radia.Services;
using Radia.Services.FileProviders;

namespace Radia.Tests
{
    public class DefaultRadiaTestContext
    {
        public const string WebHost = "http://unknownWebHost.com/";
        public const string WebRootPath = @"./webroot/path/";
        public const string SubFolderPath = @"/test";
        public string RootDirectory { get; }
        public IConfigurationService ConfigurationService { get; }
        public IContentTypeIdentifierService ContentTypeIdentifierService { get; }
        public IViewModelFactory ViewModelFactory { get; }
        public IContentProcessorFactory ContentProcessorFactory { get; }
        public IHttpContextAccessor HttpContextAccessor { get; }
        public IViewFactory ViewFactory { get; set; }
        public IRadiaFileProvider FileProvider { get; }
        public IDateTimeService DateTimeService { get; internal set; }
        public IVersionService VersionService { get; internal set; }
        public IFooterService FooterService { get; }

        public ILogger<ViewModelFactory> ViewModelFactoryLogger { get; }

        public DefaultRadiaTestContext(string rootDirectory) : this(rootDirectory,
                                                                   DefaultRadiaTestContext.DefaultFileProvider(rootDirectory)) { }

        public DefaultRadiaTestContext(string rootDirectory, IRadiaFileProvider fileProvider)
        {
            RootDirectory = rootDirectory;
            FileProvider = fileProvider;
            ConfigurationService = BuildConfigurationService(rootDirectory);
            ViewModelFactory = BuildViewModelFactory();
            ViewFactory = BuildViewFactory();
            ContentProcessorFactory = BuildContentProcessorFactory();
            ContentTypeIdentifierService = BuildContentTypeIdentifierService();
            HttpContextAccessor = MockHttpContextAccessor();
            DateTimeService = MockDateTimeService();
            VersionService = MockVersionService();
            FooterService = MockFooterService();
            ViewModelFactoryLogger = MockViewModelFactoryLogger();
        }

        private ILogger<ViewModelFactory> MockViewModelFactoryLogger()
        {
            return Mock.Of<ILogger<ViewModelFactory>>();
        }

        private IFooterService MockFooterService()
        {
            var footerService = new Mock<IFooterService>();
            footerService.Setup(x => x.GetFormattedFooter()).Returns("FormattedFooter");
            return footerService.Object;
        }

        private IVersionService MockVersionService()
        {
            var versionServiceMock = new Mock<IVersionService>();
            versionServiceMock.Setup(x => x.Version).Returns("3.0.0");
            versionServiceMock.Setup(x => x.GetVersionLinked()).Returns("<a href=\"https://github.com/DanielGilbert/Radia/releases/tag/v3.0.0\">v3.0.0</a>");
            return versionServiceMock.Object;
        }

        private IDateTimeService MockDateTimeService()
        {
            var dateTimeServiceMock = new Mock<IDateTimeService>();
            dateTimeServiceMock.Setup(x => x.Now()).Returns(new DateTime(2023, 01, 30, 21, 15, 21));
            dateTimeServiceMock.Setup(x => x.UtcNow()).Returns(new DateTime(2023, 01, 30, 19, 15, 21).ToUniversalTime());
            return dateTimeServiceMock.Object;
        }

        public static IRadiaFileProvider DefaultFileProvider(string rootDirectory)
        {
            var fileProviderFactory = new FileProviderFactory();
            var configurationService = BuildConfigurationService(rootDirectory);
            var fileProvider = fileProviderFactory.CreateComposite(configurationService.GetFileProviderConfigurations());
            return fileProvider;
        }

        private IViewModelFactory BuildViewModelFactory()
        {
            return new ViewModelFactory(FileProvider,
                                        ConfigurationService,
                                        ContentTypeIdentifierService,
                                        ContentProcessorFactory,
                                        HttpContextAccessor,
                                        new ByteSizeService(),
                                        null,
                                        this.ViewModelFactoryLogger);
            
        }

        private static IViewFactory BuildViewFactory()
        {
            return new ViewFactory();
        }

        private static IHttpContextAccessor MockHttpContextAccessor()
        {
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.Setup(p => p.HttpContext!.Request.Host).Returns(new HostString("unknownWebHost.com"));
            httpContextAccessor.Setup(p => p.HttpContext!.Request.Scheme).Returns("http");
            return httpContextAccessor.Object;
        }

        private static IContentProcessorFactory BuildContentProcessorFactory()
        {
            return new ContentProcessorFactory();
        }

        private static IContentTypeIdentifierService BuildContentTypeIdentifierService()
        {
            return new ContentTypeIdentifierService();
        }

        private static IConfigurationService BuildConfigurationService(string rootDirectory)
        {
            var myConfiguration = CreateValidConfiguration(rootDirectory);

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            var configurationService = new ConfigurationService(configuration);

            return configurationService;
        }

        private static Dictionary<string, string?> CreateValidConfiguration(string rootDirectory)
        {
            var firstFolder = rootDirectory + Path.DirectorySeparatorChar + "TestFolder1/";
            var secondFolder = rootDirectory + Path.DirectorySeparatorChar + "TestFolder2/";
            var thirdFolder = rootDirectory + Path.DirectorySeparatorChar + "TestFolder3/";

            if (Directory.Exists(firstFolder) is false)
            {
                Directory.CreateDirectory(firstFolder);
            }

            if (Directory.Exists(secondFolder) is false)
            {
                Directory.CreateDirectory(secondFolder);
            }

            if (Directory.Exists(thirdFolder) is false)
            {
                Directory.CreateDirectory(thirdFolder);
            }

            var result = new Dictionary<string, string?>
                {
                    {"AllowedHosts", "*"},
                    {"AppConfiguration:FileProviderConfigurations:0:Settings:RootDirectory", firstFolder},
                    {"AppConfiguration:FileProviderConfigurations:1:AllowListing", "false"},
                    {"AppConfiguration:FileProviderConfigurations:1:Settings:RootDirectory", secondFolder},
                    {"AppConfiguration:FileProviderConfigurations:2:AllowListing", "false"},
                    {"AppConfiguration:FileProviderConfigurations:2:Settings:RootDirectory", thirdFolder},
                    {"AppConfiguration:WebsiteTitle", "TestWebsiteTitle"},
                    {"AppConfiguration:DefaultPageHeader", "TestWebsitePageHeader" },
                    {"AppConfiguration:FooterCopyright", "&copy; Daniel Gilbert, 2009 - {{CurrentYear}}" }
                };

            return result;
        }
    }
}
