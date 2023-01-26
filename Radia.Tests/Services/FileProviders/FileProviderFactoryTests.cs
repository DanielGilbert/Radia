using Radia.Services.FileProviders;
using Radia.Services.FileProviders.Git;
using Radia.Services.FileProviders.Local;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.FileProviders;
using Moq;
using Radia.Factories;
using Radia.Services;
using Microsoft.Extensions.Configuration;

namespace Radia.Tests.Services.FileProviders
{
    public class FileProviderFactoryTests
    {
        public class RadiaTestContext
        {
            public const string GitRemoteUri = @"https://git.thisdoesnotexist.com/content.git";
            public const string LocalRootDirectory = @"/RootDirectory/";

            public FileProviderConfiguration ValidGitFileProviderConfiguration { get; }
            public FileProviderConfiguration ValidLocalFileProviderConfiguration { get; }
            public FileProviderConfiguration InvalidEnumFileProviderConfiguration { get; }
            public IRadiaFileProvider LocalFileProvider { get; }
            public IRadiaFileProvider GitFileProvider { get; }
            public IList<IRadiaFileProvider> FileProviders { get; }
            public IConfigurationService ConfigurationService { get; }
            public IFileProvider FrameworkFileProvider { get; }

            public RadiaTestContext()
            {
                ValidGitFileProviderConfiguration = CreateGitConfiguration();
                ValidLocalFileProviderConfiguration = CreateLocalConfiguration();
                InvalidEnumFileProviderConfiguration = CreateInvalidEnumConfiguration();
                ConfigurationService = MockConfigurationService();
                FrameworkFileProvider = MockFrameworkFileProvider();
                LocalFileProvider = new LocalFileProvider(ConfigurationService, FrameworkFileProvider);
                GitFileProvider = new GitFileProvider();
                FileProviders = new List<IRadiaFileProvider>()
                {
                    LocalFileProvider,
                    GitFileProvider
                };
            }

            private IFileProvider MockFrameworkFileProvider()
            {
                var fileProvider = new Mock<IFileProvider>();

                return fileProvider.Object;
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

            private static FileProviderConfiguration CreateInvalidEnumConfiguration()
            {
                var result = new FileProviderConfiguration()
                {
                    FileProvider = (FileProviderEnum)4
                };

                return result;
            }

            private static FileProviderConfiguration CreateGitConfiguration()
            {
                var result = new FileProviderConfiguration()
                {
                    FileProvider = FileProviderEnum.Git,
                    Settings = new Dictionary<string, string>()
                    {
                        { "Remote", GitRemoteUri }
                    }
                };

                return result;
            }

            private static FileProviderConfiguration CreateLocalConfiguration()
            {
                var result = new FileProviderConfiguration()
                {
                    FileProvider = FileProviderEnum.Local,
                    Settings = new Dictionary<string, string>()
                    {
                        { "RootDirectory", LocalRootDirectory }
                    }
                };

                return result;
            }

        }

        [TestClass]
        public class TheCreateMethod
        {
            public static RadiaTestContext RadiaTestContext => new();

            [DataTestMethod]
            [DataRow(FileProviderEnum.Local, typeof(LocalFileProvider), DisplayName = "Local")]
            [DataRow(FileProviderEnum.Git, typeof(GitFileProvider), DisplayName = "Git")]
            public void WhenTheConfigurationIsValid_ThenAMatchingInstanceIsReturned(FileProviderEnum fileProviderEnum, Type typeOfInstance)
            {
                var sut = new FileProviderFactory(RadiaTestContext.FileProviders);
                FileProviderConfiguration fileProviderConfiguration = fileProviderEnum switch
                {
                    FileProviderEnum.Git => RadiaTestContext.ValidGitFileProviderConfiguration,
                    FileProviderEnum.Local => RadiaTestContext.ValidLocalFileProviderConfiguration,
                    _ => throw new InvalidEnumArgumentException(nameof(fileProviderEnum), (int)fileProviderEnum, typeof(FileProviderEnum))
                };

                var result = sut.Create(fileProviderConfiguration);

                result.Should().NotBeNull();
                result.Should().BeOfType(typeOfInstance);
            }

            [TestMethod]
            public void WhenFileProviderEnumIsInvalid_ThenAnInvalidEnumExceptionIsThrown()
            {
                var sut = new FileProviderFactory(RadiaTestContext.FileProviders);
                var fileProviderConfiguration = new FileProviderConfiguration()
                {
                    FileProvider = (FileProviderEnum)4
                };

                sut.Invoking(sut => sut.Create(fileProviderConfiguration))
                   .Should()
                   .Throw<InvalidEnumArgumentException>();
            }
        }
    }
}
