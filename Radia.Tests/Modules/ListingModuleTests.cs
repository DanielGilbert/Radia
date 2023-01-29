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
      

        [TestClass]
        public class TheProcessRequestMethod
        {
            public TestContext? TestContext { get; set; }
            DefaultRadiaTestContext RadiaTestContext { get; set; }

            public TheProcessRequestMethod()
            {
                RadiaTestContext = new DefaultRadiaTestContext(FileProviderEnum.Local, TestContext.TestRunDirectory);
            }

            [TestInitialize]
            public void InitializeTest()
            {
                RadiaTestContext = new DefaultRadiaTestContext(FileProviderEnum.Local, TestContext.TestRunDirectory);
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
            public TestContext? TestContext { get; set; }
            DefaultRadiaTestContext RadiaTestContext { get; }

            public TheProcessRequestWithArgsMethod()
            {
                RadiaTestContext = new DefaultRadiaTestContext(FileProviderEnum.Local, TestContext.TestRunDirectory);
            }

            [TestMethod]
            public void WhenCalled_ThenWillReturnTheFolderViewModelAndWillBeInTheGivenFolder()
            {
                var sut = new ListingModule(RadiaTestContext.WebHostEnvironment,
                                            RadiaTestContext.FileProviderFactory,
                                            RadiaTestContext.ViewModelFactory,
                                            RadiaTestContext.ViewFactory,
                                            RadiaTestContext.ConfigurationService);

                var result = sut.ProcessRequest(DefaultRadiaTestContext.SubFolderPath);

                result.Should().NotBeNull();
            }
        }
    }
}
