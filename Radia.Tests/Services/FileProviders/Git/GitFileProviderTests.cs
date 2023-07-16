using FluentAssertions;
using Radia.Services.FileProviders.Git;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radia.Tests.Services.FileProviders.Git
{
    public class GitFileProviderTests
    {
        [TestClass]
        public class TheGetDirectoryContentMethod
        {
            public TestContext? TestContext { get; set; }

            [DataRow("")]
            [DataRow("/")]
            [DataRow("articles/[001]-building-a-webengine.md")]
            [DataRow("articles/")]
            [DataRow("readme.md")]
            [DataTestMethod]
            public void WhenReceivingTheRoot_ThenWillReturnAllFiles(string rootDirectory)
            {
                string repository = TestContext!.DeploymentDirectory + Path.DirectorySeparatorChar + "GitRepository";
                foreach (var entry in Directory.GetFiles(repository))
                {
                    Console.WriteLine(entry);
                }
                string localCache = TestContext!.TestRunDirectory + Path.DirectorySeparatorChar + Path.GetRandomFileName();
                Directory.CreateDirectory(localCache);
                GitFileProviderSettings gitFileProviderSettings = new GitFileProviderSettings(repository, "main", localCache, true);
                GitFileProvider gitFileProvider = new GitFileProvider(gitFileProviderSettings);
                gitFileProvider.Fetch();
                var result = gitFileProvider.GetDirectoryContents(rootDirectory);

                result.Should().NotBeEmpty();
                result.Where((x) => x.Name.EndsWith(".md")).Should().NotBeEmpty();
            }
        }
    }
}
