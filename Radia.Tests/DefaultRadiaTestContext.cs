using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Moq;
using Radia.Services.FileProviders.Local;
using Radia.Services.FileProviders;
using Radia.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Radia.Factories;
using System.ComponentModel;

namespace Radia.Tests
{
    public class DefaultRadiaTestContext
    {
        public const string WebHost = "http://unknownWebHost.com/";
        public const string WebRootPath = @"./webroot/path/";
        public const string SubFolderPath = @"/test";

        public string RootDirectory { get; }

        public IFileProviderConfiguration ValidFileProviderConfiguration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }
        public IRadiaFileProviderFactory FileProviderFactory { get; }
        public IRadiaFileProvider LocalFileProvider { get; }
        public IRadiaFileProvider EmptyFileProvider { get; }
        public IConfigurationService ConfigurationService { get; }
        public IContentTypeIdentifierService ContentTypeIdentifierService { get; }
        public IViewModelFactory ViewModelFactory { get; }
        public IContentProcessorFactory<string> ContentProcessorFactory { get; }
        public IHttpContextAccessor HttpContextAccessor { get; }
        public IViewFactory ViewFactory { get; set; }
        public IFileInfo IndexFileInfo { get; set; }
        public IFileInfo SubFolderFileInfo { get; set; }
        public IList<IRadiaFileProvider> FileProviders { get; }

        public DefaultRadiaTestContext(FileProviderEnum fileProviderEnum, string rootDirectory)
        {
            RootDirectory = rootDirectory;
            ValidFileProviderConfiguration = CreateFileProviderConfiguration(fileProviderEnum);
            IndexFileInfo = MockIndexFileInfo();
            SubFolderFileInfo = MockSubFolderFileInfo();
            HttpContextAccessor = MockHttpContextAccessor();
            ContentProcessorFactory = BuildContentProcessorFactory();
            WebHostEnvironment = MockWebHostEnvironment();
            LocalFileProvider = BuildLocalFileProvider();
            EmptyFileProvider = BuildEmptyFileProvider();
            ConfigurationService = MockConfigurationService();
            ContentTypeIdentifierService = BuildContentTypeIdentifierService();
            FileProviderFactory = BuildFileProviderFactory();
            ViewFactory = BuildViewFactory();
            
            FileProviders = new List<IRadiaFileProvider>()
                        {
                            LocalFileProvider,
                            EmptyFileProvider
                        };
            ViewModelFactory = new ViewModelFactory(FileProviderFactory,
                                                    ConfigurationService,
                                                    ContentTypeIdentifierService,
                                                    ContentProcessorFactory,
                                                    HttpContextAccessor,
                                                    ValidFileProviderConfiguration);
        }

        private IFileProviderConfiguration CreateFileProviderConfiguration(FileProviderEnum fileProviderEnum)
        {
            return fileProviderEnum switch
            {
                FileProviderEnum.Local => GenerateLocalFileProviderConfiguration(),
                FileProviderEnum.Empty => GenerateEmptyFileProviderConfiguration(),
                _ => throw new InvalidEnumArgumentException(nameof(fileProviderEnum), (int)fileProviderEnum, typeof(FileProviderEnum))
            };
        }

        private static IFileProviderConfiguration GenerateEmptyFileProviderConfiguration()
        {
            var result = new FileProviderConfiguration()
            {
                FileProvider = FileProviderEnum.Empty
            };

            return result;
        }

        private IFileProviderConfiguration GenerateLocalFileProviderConfiguration()
        {
            var result = new FileProviderConfiguration()
            {
                FileProvider = FileProviderEnum.Local,
                Settings = new Dictionary<string, string>()
                        {
                            { "RootDirectory", RootDirectory }
                        }
            };

            return result;
        }

        private static IFileInfo MockSubFolderFileInfo()
        {
            var indexFileInfo = new Mock<IFileInfo>();
            indexFileInfo.Setup(x => x.Exists).Returns(true);
            indexFileInfo.Setup(x => x.IsDirectory).Returns(true);
            return indexFileInfo.Object;
        }

        private static IFileInfo MockIndexFileInfo()
        {
            var indexFileInfo = new Mock<IFileInfo>();
            indexFileInfo.Setup(x => x.Exists).Returns(true);
            indexFileInfo.Setup(x => x.IsDirectory).Returns(true);
            return indexFileInfo.Object;
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

        private IRadiaFileProvider BuildLocalFileProvider()
        {
            return new LocalFileProvider(ValidFileProviderConfiguration);
        }

        private static IRadiaFileProvider BuildEmptyFileProvider()
        {
            return new EmptyFileProvider();
        }


        private IRadiaFileProviderFactory BuildFileProviderFactory()
        {
            var fileProviders = new List<IRadiaFileProvider>
                {
                    LocalFileProvider,
                    EmptyFileProvider
                };
            var factory = new FileProviderFactory(fileProviders);

            return factory;
        }

        private static IWebHostEnvironment MockWebHostEnvironment()
        {
            var webHostEnvironment = new Mock<IWebHostEnvironment>();

            webHostEnvironment.Setup((env) => env.WebRootPath).Returns(WebRootPath);

            return webHostEnvironment.Object;
        }
    }
}
