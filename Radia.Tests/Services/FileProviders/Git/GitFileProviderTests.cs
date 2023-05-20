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

            [TestMethod]
            public void WhenReceivingTheRoot_ThenWillReturnAllFiles()
            {
                string repository = TestContext.DeploymentDirectory + "\\GitRepository";
                string localCache = TestContext.TestRunDirectory + "\\" + Path.GetRandomFileName();
                Directory.CreateDirectory(localCache);
                GitFileProviderSettings gitFileProviderSettings = new GitFileProviderSettings(repository, "main", localCache);
                GitFileProvider gitFileProvider = new GitFileProvider(gitFileProviderSettings, true);
            }
        }
    }
}
