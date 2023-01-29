using Radia.Services.FileProviders;
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


        [TestClass]
        public class TheCreateMethod
        {
            public TestContext? TestContext { get; set; }

            [DataTestMethod]
            [DataRow(FileProviderEnum.Local, DisplayName = "Local")]
            public void WhenTheConfigurationIsValid_ThenAMatchingInstanceIsReturned(FileProviderEnum fileProviderEnum)
            {
                DefaultRadiaTestContext RadiaTestContext = new(fileProviderEnum, TestContext.TestRunDirectory);

                var sut = new FileProviderFactory(RadiaTestContext.FileProviders);
                var result = sut.Create(RadiaTestContext.ValidFileProviderConfiguration);

                result.Should().NotBeNull();
                result.FileProviderEnum.Should().Be(fileProviderEnum);
            }

            [TestMethod]
            public void WhenFileProviderEnumIsInvalid_ThenAnInvalidEnumExceptionIsThrown()
            {
                ///Actually, it doesn't matter which Configuration is provided here,
                ///as it just sets the test context.
                DefaultRadiaTestContext RadiaTestContext = new(FileProviderEnum.Empty, TestContext.TestRunDirectory);
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
