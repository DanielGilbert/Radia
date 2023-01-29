using FluentAssertions;
using Radia.Models;
using Radia.Services.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radia.Tests.Models
{
    public class RadiaFileInfoTests
    {
        [TestClass]
        public class TheCtorMethod
        {
            public TestContext? TestContext { get; set; }

            [TestMethod]
            public void GivenAPathOfFiveFolders_ThenReturnAncestorsListOfFour()
            {
                var defaultRadiaTestContext = new DefaultRadiaTestContext(FileProviderEnum.Local, TestContext.TestRunDirectory);

                string testDirectory = @"test\test2\test3\test4";

                var testFolder = Path.Combine(TestContext.TestRunDirectory, testDirectory);

                Directory.CreateDirectory(testFolder);

                var fileProvider = defaultRadiaTestContext.FileProviderFactory.Create(defaultRadiaTestContext.ValidFileProviderConfiguration);
                var parent = testDirectory.TrimEnd('\\')[..testDirectory.LastIndexOf('\\')];
                var result = fileProvider.GetDirectoryContents(parent);
                foreach(var data in result)
                {
                    RadiaFileInfo radiaFileInfo = new (DefaultRadiaTestContext.WebHost, data, testDirectory, Path.DirectorySeparatorChar);
                    radiaFileInfo.Should().NotBeNull();
                }
            }

        }
    }
}
