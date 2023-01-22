using Dagidirli.Services.FileProviders;
using Dagidirli.Services.FileProviders.Git;
using Dagidirli.Services.FileProviders.Local;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dagidirli.Tests.Services.FileProviders
{
    public class FileProviderFactoryTests
    {
        public class DagidirliTestContext
        {
            public const string GitRemoteUri = @"https://git.thisdoesnotexist.com/content.git";
            public const string LocalRootDirectory = @"/RootDirectory/";

            public FileProviderConfiguration ValidGitFileProviderConfiguration { get; }
            public FileProviderConfiguration ValidLocalFileProviderConfiguration { get; }

            public DagidirliTestContext()
            {
                ValidGitFileProviderConfiguration = CreateGitConfiguration();
                ValidLocalFileProviderConfiguration = CreateLocalConfiguration();
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
            public static DagidirliTestContext DagidirliTestContext => new();

            [DataTestMethod]
            [DataRow(FileProviderEnum.Local, typeof(LocalFileProvider), DisplayName = "Local")]
            [DataRow(FileProviderEnum.Git, typeof(GitFileProvider), DisplayName = "Git")]
            public void WhenTheConfigurationIsValid_AMatchingInstanceIsReturned(FileProviderEnum fileProviderEnum, Type typeOfInstance)
            {
                var sut = new FileProviderFactory();
                FileProviderConfiguration fileProviderConfiguration = fileProviderEnum switch
                {
                    FileProviderEnum.Git => DagidirliTestContext.ValidGitFileProviderConfiguration,
                    FileProviderEnum.Local => DagidirliTestContext.ValidLocalFileProviderConfiguration,
                    _ => throw new InvalidEnumArgumentException(nameof(fileProviderEnum), (int)fileProviderEnum, typeof(FileProviderEnum))
                };

                var result = sut.Create(fileProviderConfiguration);

                result.Should().NotBeNull();
                result.Should().BeOfType(typeOfInstance);
            }
        }
    }
}
