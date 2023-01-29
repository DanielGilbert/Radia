using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using Radia.Factories;
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

        public DefaultRadiaTestContext(string rootDirectory, IRadiaFileProvider fileProvider)
        {
            RootDirectory = rootDirectory;
            FileProvider = fileProvider;
            ConfigurationService = BuildConfigurationService();
            ViewModelFactory = BuildViewModelFactory();
            ViewFactory = BuildViewFactory();
            ContentProcessorFactory = BuildContentProcessorFactory();
            ContentTypeIdentifierService = BuildContentTypeIdentifierService();
            HttpContextAccessor = MockHttpContextAccessor();
        }

        public static IRadiaFileProvider DefaultFileProvider()
        {
            var fileProviderFactory = new FileProviderFactory();
            var configurationService = BuildConfigurationService();
            var fileProvider = fileProviderFactory.CreateComposite(configurationService.GetFileProviderConfigurations());
            return fileProvider;
        }

        private IViewModelFactory BuildViewModelFactory()
        {
            return new ViewModelFactory(FileProvider,
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

        private static IConfigurationService BuildConfigurationService()
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
                    {"AppConfiguration:FileProviderConfigurations:0:Settings:RootDirectory", "/content/"},
                    {"AppConfiguration:FileProviderConfigurations:1:AllowListing", "false"},
                    {"AppConfiguration:FileProviderConfigurations:1:Settings:RootDirectory", "/app/templates/default/views/"},
                    {"AppConfiguration:FileProviderConfigurations:2:AllowListing", "false"},
                    {"AppConfiguration:FileProviderConfigurations:2:Settings:RootDirectory", "/app/templates/default/"},
                    {"AppConfiguration:WebsiteTitle", "TestWebsiteTitle"},
                    {"AppConfiguration:DefaultPageHeader", "TestWebsitePageHeader" }
                };

            return result;
        }
    }
}
