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
        [TestClass]
        public class TheCreateMethod
        {
            public static DefaultRadiaTestContext RadiaTestContext => new();

            [DataTestMethod]
            [DataRow(FileProviderEnum.Local, DisplayName = "Local")]
            [DataRow(FileProviderEnum.Git, DisplayName = "Git")]
            public void WhenTheConfigurationIsValid_ThenAMatchingInstanceIsReturned(FileProviderEnum fileProviderEnum)
            {
                var sut = new FileProviderFactory(RadiaTestContext.FileProviders);
                FileProviderConfiguration fileProviderConfiguration = fileProviderEnum switch
                {
                    FileProviderEnum.Git => RadiaTestContext.ValidGitFileProviderConfiguration,
                    FileProviderEnum.Local => RadiaTestContext.ValidLocalFileProviderConfiguration,
                };

                var result = sut.Create(fileProviderConfiguration);

                result.Should().NotBeNull();
                result.FileProviderEnum.Should().Be(fileProviderEnum);
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
